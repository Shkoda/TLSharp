using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core.Utils;

namespace TLSharp.Core.Auth.SecureChat
{
    public class NewChatRequest
    {
        public static async Task<TLEncryptedChat> RequestChat(TelegramClient client, int recepientId, byte[] ga)
        {
            var chatResponce = await client.SendRequestAsync<TLAbsEncryptedChat>(new TLRequestRequestEncryption
            {
                g_a = ga,
                random_id = (int)Helpers.GenerateRandomLong(),
                user_id = new TLInputUser
                {
                    user_id = recepientId
                }
            });
            if (chatResponce.GetType() == typeof(TLEncryptedChat))
                return chatResponce as TLEncryptedChat;

            while (true)
            {
                var somethingFromServer = await client.Receive();

                var updates = (TLUpdates)somethingFromServer;
                var acceptedChat =
                    (updates?.updates.lists.FirstOrDefault(u => u.GetType() == typeof(TLUpdateEncryption))) as
                        TLUpdateEncryption;

                if (acceptedChat != null)
                {
                    Console.WriteLine($"Chat accept: TLUpdateEncryption was received");
                    return (TLEncryptedChat)acceptedChat.chat;
                }
            }
        }
    }
}
