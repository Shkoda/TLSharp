using System;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core.MTProto.Crypto;

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
        private static TLEncryptedChat _acceptedChat;
        private static TelegramClient _client;

        public static async Task StartSercretChat(TelegramClient client)
        {
            _client = client;
            var dhConfig = await DHConfigReceiver.GetConfig(client);
            var newConfig = dhConfig as TLDhConfig;

            _dhPrime = new BigInteger(1, newConfig.p);
            _a = new BigInteger(2048, new Random());
            _ga = BigInteger.ValueOf(newConfig.g).ModPow(_a, _dhPrime);

          
            _acceptedChat = await NewChatRequest.RequestChat(client, -2, _ga.ToByteArray());

            _gb = new BigInteger(1, _acceptedChat.g_a_or_b);
            _gab = _gb.ModPow(_a, _dhPrime);
            _authKey = new AuthKey(_gab);

            while (true)
            {
                try
                {
                    var r = await client.Receive();
                    if (r.GetType() == typeof (TLUpdates))
                    {
                        ((TLUpdates) r).updates?.lists?.ForEach(async u => await HandleUpdate(u));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"EXCEPTION: {e.Message}");
                    Console.WriteLine($"{e.StackTrace}");
                    Console.WriteLine($"Continue execution");
                }
            }
        }

        private static async Task SendDebugMessage()
        {
            Console.WriteLine("start sending debug message... ");
            var sendEncryptedMessageRequest =
                EncryptedRequestBuilder.CreateSendEncryptedMessageRequest("I like eels", _acceptedChat, _authKey);

            await _client.SendRequest(sendEncryptedMessageRequest);
            Console.WriteLine("debug message was sent");
        }

        private static async Task HandleUpdate(TLAbsUpdate absUpdate)
        {
            if (absUpdate.GetType() == typeof (TLUpdateNewEncryptedMessage))
            {
                var newEncryptedMessage = absUpdate as TLUpdateNewEncryptedMessage;
                if (newEncryptedMessage.message.GetType() == typeof (TLEncryptedMessage))
                {
                    await HandleEncryptedMessge(newEncryptedMessage.message as TLEncryptedMessage);
                }
                else
                {
                    HandleEncryptedMessageService(newEncryptedMessage.message as TLEncryptedMessageService);
                }
            }
            else
            {
                Console.WriteLine($"skipping update {absUpdate.GetType()}");
            }
        }

        private static async Task HandleEncryptedMessge(TLEncryptedMessage encryptedMessage)
        {
            var decrypted = SecretChatDecryptor.Decrypt(encryptedMessage.bytes, _authKey.Data);
            if (decrypted.GetType() == typeof (TLDecryptedMessageLayer8))
            {
                var message = decrypted as TLDecryptedMessageLayer8;
                Console.WriteLine($"Decrypted message: {message.message}");
                if (message.message == "Say")
                {
                    await SendDebugMessage();
                }
            }
            else
            {
                Console.WriteLine($"We have decrypted message of type {decrypted.GetType()}");
            }
        }

        private static void HandleEncryptedMessageService(TLEncryptedMessageService encryptedMessage)
        {
            var decrypted = SecretChatDecryptor.Decrypt(encryptedMessage.bytes, _authKey.Data);
            var msg = decrypted != null
                ? $"We have received service message of type {decrypted.GetType()}"
                : $"We have failed to parse service message with constructor";

            Console.WriteLine(msg);
        }
    }
}