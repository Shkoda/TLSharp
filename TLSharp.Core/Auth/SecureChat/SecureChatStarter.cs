using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core.MTProto.Crypto;
using TLSharp.Core.Utils;
using static System.Int32;

namespace TLSharp.Core.Auth.SecureChat
{
    public class SecureChatStarter
    {
        private static BigInteger _dhPrime;
        private static BigInteger _a;
        private static BigInteger _ga;
        private static BigInteger _gb;
        private static BigInteger _gab;
        private static AuthKey _authKey;

        public static async Task StartSercretChat(TelegramClient client)
        {
            var dhConfig = await GetConfig(client);

            var newConfig = dhConfig as TLDhConfig;

            _dhPrime = new BigInteger(1, newConfig.p);
            _a = new BigInteger(2048, new Random());
            _ga = BigInteger.ValueOf(newConfig.g).ModPow(_a, _dhPrime);

            var acceptedChat = await RequestChat(client, -1); //todo use real user id


            _gb = new BigInteger(1, acceptedChat.g_a_or_b);
            _gab = _gb.ModPow(_a, _dhPrime);
            _authKey = new AuthKey(_gab);

            var textMessageId = Helpers.GenerateRandomLong();
            var plainMessage = GetGefaultPlainMessage(textMessageId);
            var plainBytesAndLength = SerializePlainMessage(plainMessage);

            var msgKey = Helpers.CalcMsgKey(plainBytesAndLength);
            var aesKeyData = Helpers.CalcKey(_authKey.Data, msgKey, true);

            var paddedPlainBytes = BuildPaddedPlainBytes(plainBytesAndLength);
            var encryptedBytes = AES.EncryptAES(aesKeyData, paddedPlainBytes);

            var chipherBytesWithKeys = GetEncryptedBytesWithKeys(encryptedBytes, msgKey, _authKey.Id);

            var sendEncryptedMessageRequest = CreateSendEncryptedMessageRequest(chipherBytesWithKeys, acceptedChat,
                textMessageId);

            var sentMessage = await client.SendRequestAsync<TLSentEncryptedMessage>(sendEncryptedMessageRequest);

            while (true)
            {
                var r = await client.Receive();
                if (r.GetType() != typeof (TLUpdateShort))
                    Console.WriteLine($"received {r.GetType()}");
            }


            Console.WriteLine($"acceptedChat: {acceptedChat.GetType()} ");
            Console.WriteLine($"sent message {sentMessage.Constructor}");
        }

        private static byte[] BuildPaddedPlainBytes(byte[] plainBytesAndLength)
        {
            var plainLength = plainBytesAndLength.Length;
            var padding = (16 - plainLength%16) % 16;
            var paddedPlainBytesAndLength = new byte[plainLength + padding];

            Array.Copy(plainBytesAndLength, 0, paddedPlainBytesAndLength, 0, plainLength);

            var paddingBytes = Helpers.GenerateRandomBytes(padding);
            Array.Copy(paddingBytes, 0, paddedPlainBytesAndLength, plainLength, padding);
            return paddedPlainBytesAndLength;
        }

        private static TLRequestSendEncrypted CreateSendEncryptedMessageRequest(byte[] chipherBytesWithKeys,
            TLEncryptedChat acceptedChat, long textMessageId)
        {
            var sendEncryptedMessageRequest = new TLRequestSendEncrypted
            {
                data = chipherBytesWithKeys,
                peer = new TLInputEncryptedChat
                {
                     access_hash = acceptedChat.access_hash,
                    chat_id = acceptedChat.id
                },
                random_id = textMessageId
            };
            return sendEncryptedMessageRequest;
        }

        private static byte[] GetEncryptedBytesWithKeys(byte[] encryptedBytes, byte[] msgKey, ulong keyFingerprint)
        {
            var length = 8 + 16 + encryptedBytes.Length;
            byte[] chipherBytesWithKeys;
            using (var ciphertextPacket = new MemoryStream(new byte[length], 0, length, true, true))
            {
                using (var writer = new BinaryWriter(ciphertextPacket))
                {
                    writer.Write(keyFingerprint);
                    writer.Write(msgKey);
                    writer.Write(encryptedBytes);

                    chipherBytesWithKeys = (ciphertextPacket.GetBuffer());
                }
            }
            return chipherBytesWithKeys;
        }

        private static byte[] SerializePlainMessage(TLDecryptedMessageLayer plainMessage)
        {
            var plainBytes = plainMessage.Serialize();
            byte[] plainBytesAndLength;

            var paddedArrayLength = 4 + plainBytes.Length;
            using (var plaintextPacket = new MemoryStream(new byte[paddedArrayLength], 0, paddedArrayLength, true,true))
            {
                using (var plaintextWriter = new BinaryWriter(plaintextPacket))
                {
                    plaintextWriter.Write(plainBytes.Length);
                    plaintextWriter.Write(plainBytes);
                    plainBytesAndLength = plaintextPacket.GetBuffer();
                }
            }
            return plainBytesAndLength;
        }

        private static TLDecryptedMessageLayer GetGefaultPlainMessage(long textMessageId)
        {
            var plainMessage = new TLDecryptedMessageLayer
            {
                layer = 23,
                message = new TLDecryptedMessage
                {
                    random_id = textMessageId,
                    message = "I like eels",
                    media = new TLDecryptedMessageMediaEmpty(),
                    ttl = 1200
                   },
                random_bytes = Helpers.GenerateRandomBytes(20),
                in_seq_no = 0,
                out_seq_no = 1
            };
            return plainMessage;
        }
       

        private static async Task<TLEncryptedChat> RequestChat(TelegramClient client, int recepientId)
        {
            var chatResponce = await client.SendRequestAsync<TLAbsEncryptedChat>(new TLRequestRequestEncryption
            {
                g_a = _ga.ToByteArray(),
                random_id = (int) Helpers.GenerateRandomLong(),
                user_id = new TLInputUser
                {
                    user_id = recepientId
                }
            });
            if (chatResponce.GetType() == typeof (TLEncryptedChat))
                return chatResponce as TLEncryptedChat;

            while (true)
            {
                var somethingFromServer = await client.Receive();

                if (somethingFromServer.GetType() == typeof (TLUpdateShort))
                {
                    Console.WriteLine($"short update");
                    continue;
                }

                var updates = (TLUpdates) somethingFromServer;
                var acceptedChat =
                    (updates.updates.lists.FirstOrDefault(u => u.GetType() == typeof (TLUpdateEncryption))) as
                        TLUpdateEncryption;

                if (acceptedChat != null)
                    return (TLEncryptedChat) acceptedChat.chat;
            }
        }

        
        private static async Task<TLAbsDhConfig> GetConfig(TelegramClient client)
        {
            var getConfigResponce = await client.SendRequestAsync<TLAbsDhConfig>(new TLRequestGetDhConfig());
            if (getConfigResponce is TLDhConfig)
            {
                var newConfig = getConfigResponce as TLDhConfig;
                Console.WriteLine($"new dh config received");
                return newConfig;
            }
            if (getConfigResponce is TLDhConfigNotModified)
            {
                var notMoodifiedewConfig = getConfigResponce as TLDhConfigNotModified;
                Console.WriteLine($"not modified config received {notMoodifiedewConfig.random}");
                return notMoodifiedewConfig;
            }
            Console.WriteLine($"failed to obtain dh config");
            return null;
        }
    }
}