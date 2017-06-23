using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core;
using TLSharp.Core.Utils;

namespace ConsoleApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            LaunchTLClient().Wait();
        }

        private static async Task LaunchTLClient()
        {
            var creds = CredentialReader.Load();
         
            var client = new TelegramClient(creds.AppId, creds.AppHash);
            await client.ConnectAsync();
            if (!client.IsUserAuthorized())
            {           
                Console.WriteLine($"send code to {creds.PhoneNumber}");
            
                var hash = await client.SendCodeRequestAsync(creds.PhoneNumber);
                Console.WriteLine("enter recived code");
                var code = Console.ReadLine();
    
                var user = await client.MakeAuthAsync(creds.PhoneNumber, hash, code);
                Console.WriteLine($"auth as @{user.username} [{user.first_name}] id={user.id}");
            }

      
        //    Console.WriteLine($"starting secret chat...");
         //   await client.StartSecretChat();
        //    await ShowContacts(client);


            var keyboard = new TLReplyKeyboardMarkup
            {
               rows = new TLVector<TLKeyboardButtonRow>
                {
                    lists = new List<TLKeyboardButtonRow>
                    {
                        new TLKeyboardButtonRow
                        {
                            buttons = new TLVector<TLAbsKeyboardButton>
                            {
                                lists = new List<TLAbsKeyboardButton>
                                {
                                    new TLKeyboardButton()
                                    {
                                        text = "yooohooo"
                                    },
                                    new TLKeyboardButton()
                                    {
                                        text = "woooow"
                                    }
                                }
                            }
                        }
                    }
                }
                
            };
   

            var resp = await client.SendRequestAsync<TLObject>(new TLRequestSendMessage
            {
                peer = new TLInputPeerUser
                {
                    user_id = -1,
                   
                },
                message = "it is c# =) qwerty",
                random_id = Helpers.GenerateRandomLong(),
                reply_markup = keyboard,
                
                entities = new TLVector<TLAbsMessageEntity>
                {
                    lists = new List<TLAbsMessageEntity>
                    {
                        new TLMessageEntityBold
                        {
                            offset = 0,
                            length = 8
                        },
                    
                    }
                }
                
                
            });

            while (true)
            {
                Console.WriteLine($"waiting...");
                var upd = await client.Receive();
                Console.WriteLine($"update {upd.GetType()}");
            }
            Console.WriteLine($"Press enter to terminate...");
            Console.ReadLine();
        }

        private static async Task ShowContacts(TelegramClient client)
        {
            var result = await client.GetContactsAsync();


            result.users.lists
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                  .ToList()
                 .ForEach(u => Console.WriteLine($"@{u.username} [{u.first_name}] {u.phone} {u.id}"));

            
        }
    }
}