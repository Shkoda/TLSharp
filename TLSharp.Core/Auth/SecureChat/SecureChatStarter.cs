using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core.MTProto.Crypto;
using TLSharp.Core.Network;
using TLSharp.Core.Utils;

namespace TLSharp.Core.Auth.SecureChat
{
    public class SecureChatStarter
    {
        private static BigInteger dh_prime;
        private static BigInteger a;
        private static BigInteger _ga;
        private static int _randomId;
        private static BigInteger _gb;
        private static BigInteger _key;

        public static async Task StartSercretChat(TcpTransport transport, Session session, TelegramClient client)
        {
            var dhConfig = await GetConfig(client);

            var newConfig = dhConfig as TLDhConfig;
            dh_prime = new BigInteger(1, newConfig.p);
            a = new BigInteger(2048, new Random());
            _ga = BigInteger.ValueOf(newConfig.g).ModPow(a, dh_prime);
            _randomId = new Random().Next(Int32.MaxValue);
      
            var chat =  await client.SendRequestAsync<TLAbsEncryptedChat>(new TLRequestRequestEncryption
            {
                g_a = _ga.ToByteArray(),
                random_id = _randomId,
                user_id = new TLInputUser
                {
                    user_id = -1 //todo use real user id
                }
            });
            var updates = await client.Receive();
            
            

         var updateEncryption =  (TLUpdateEncryption)  updates.updates.lists[0];
        var acceptedChat =  (TLEncryptedChat) updateEncryption.chat;

            _gb = new BigInteger(1, acceptedChat.g_a_or_b);
            _key = _gb.ModPow(a, dh_prime);
 
         
            var plain = new TLDecryptedMessage
            {
                message = "I like eels"
            };
            var decryptedMessage = new TLDecryptedMessageLayer
            {
                layer = 23,
                message = plain,
                random_bytes = new BigInteger(20, new Random()).ToByteArray()
            };
            
      
            var keyBytes = _key.ToByteArrayUnsigned();
            var paddedKeyBytes = new byte[256];
            Array.Copy(_key.ToByteArrayUnsigned(), 0, paddedKeyBytes, paddedKeyBytes.Length-keyBytes.Length, keyBytes.Length);
            
            byte[] msgKey;
            using (SHA1 hash = new SHA1Managed())
            {
                using (MemoryStream hashStream = new MemoryStream(hash.ComputeHash(paddedKeyBytes), false))
                {
                    using (BinaryReader hashReader = new BinaryReader(hashStream))
                    {
                      msgKey = hashReader.ReadBytes(16);
         
                    }
                }
            }
            var aesKey = Helpers.CalcKey(_key.ToByteArrayUnsigned(), msgKey, false);


            byte[] decryptedBytes;
            using (var memory = new MemoryStream())
            using (var writer = new BinaryWriter(memory))
            {
                decryptedMessage.SerializeBody(writer);
               decryptedBytes =  memory.ToArray();
            }

            
            
            var encryptAes = AES.EncryptAES(aesKey, decryptedBytes);

         var sendEncryptedMessageRequest = new TLRequestSendEncrypted
            {
                data = encryptAes,
                peer = new TLInputEncryptedChat
                {
                    access_hash = acceptedChat.access_hash,
                    chat_id = acceptedChat.id
                }
                ,
                random_id = Helpers.GenerateRandomLong()
            };

           var sentMessage = await client.SendRequestAsync<TLSentEncryptedMessage>(sendEncryptedMessageRequest);
     

            Console.WriteLine($"chat: {chat.GetType()} access_hash={chat.a});
            Console.WriteLine($"sent message {sentMessage.Constructor}");
        }

        private static async Task<TLAbsDhConfig> GetConfig(TelegramClient client)
        {
            var getConfigResponce = await client.SendRequestAsync<TLAbsDhConfig>(new TLRequestGetDhConfig());
            if (getConfigResponce is TLDhConfig)
            {
                var newConfig = getConfigResponce as TLDhConfig;
                Console.WriteLine($"new dh config received: g={newConfig.g}, dh_prime={ToString(newConfig.p)}");
                return newConfig;
            }
            else if (getConfigResponce is TLDhConfigNotModified)
            {
                var notMoodifiedewConfig = getConfigResponce as TLDhConfigNotModified;
                Console.WriteLine($"not modified config received {notMoodifiedewConfig.random}");
                return notMoodifiedewConfig;
            }
            else
            {
                Console.WriteLine($"failed to obtain dh config");
                return null;
            }

        }

        private static string ToString<T>(T[] arr)
        {
            return arr.Aggregate("", (acc, current) => acc += current.ToString() + " ");
        }
    }
}
