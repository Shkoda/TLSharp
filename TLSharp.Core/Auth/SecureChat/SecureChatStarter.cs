using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TeleSharp.TL.Updates;
using TLSharp.Core.MTProto.Crypto;
using TLSharp.Core.Network;

namespace TLSharp.Core.Auth.SecureChat
{
    public class SecureChatStarter
    {
        private static BigInteger p;
        private static BigInteger a;
        private static BigInteger _ga;
        private static int _randomId;

        public static async Task StartSercretChat(TcpTransport transport, Session session, TelegramClient client)
        {
            var dhConfig = await GetConfig(client);

            var newConfig = dhConfig as TLDhConfig;
            p = new BigInteger(1, newConfig.p);
            a = new BigInteger(2048, new Random());
            _ga = BigInteger.ValueOf(newConfig.g).ModPow(a, p);
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

            var state = await client.SendRequestAsync<TLState>(new TLRequestGetState());
            

           TLDifference diff;

            while (true)
            {
                var result = await client.SendRequestAsync<TLAbsDifference>(new TLRequestGetDifference
                {
                    pts = state.pts,
                    qts = state.qts,
                    date = state.date
                });
            
                diff = result as TLDifference;
                if (diff == null)
                {
                    Console.WriteLine("no diff...");
                    Thread.Sleep(500);
                    continue;
                }
                break;
            } 
            Console.WriteLine($"diff received! constr = {diff.Constructor}");


           

            Console.WriteLine($"chat: {chat.GetType()}");
        }

        private static async Task<TLAbsDhConfig> GetConfig(TelegramClient client)
        {
            var getConfigResponce = await client.SendRequestAsync<TLAbsDhConfig>(new TLRequestGetDhConfig());
            if (getConfigResponce is TLDhConfig)
            {
                var newConfig = getConfigResponce as TLDhConfig;
                Console.WriteLine($"new dh config received: g={newConfig.g}, p={ToString(newConfig.p)}");
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
