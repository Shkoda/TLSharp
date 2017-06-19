using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeleSharp.TL;
using TLSharp.Core;

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
                Console.WriteLine($"auth as @{user.username} [{user.first_name}]");
            }
      

            try
            {
                //get available contacts
                var result = await client.GetContactsAsync();

            
                    result.users.lists
                        .Where(x => x.GetType() == typeof(TLUser))
                        .Cast<TLUser>()
                          .ToList()
                         .ForEach(u => Console.WriteLine($"@{u.username} [{u.first_name}] {u.phone}"));
                  


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw;
            }
        

        }
    }
}