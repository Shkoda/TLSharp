using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL.Messages;

namespace TLSharp.Core.Auth.SecureChat
{
    public class DHConfigReceiver
    {
        public static async Task<TLAbsDhConfig> GetConfig(TelegramClient client)
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
