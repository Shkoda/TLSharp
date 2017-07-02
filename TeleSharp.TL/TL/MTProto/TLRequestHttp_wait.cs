using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1835453025)]
    public class TLRequestHttp_wait : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1835453025;
            }
        }

                public int max_delay {get;set;}
        public int wait_after {get;set;}
        public int max_wait {get;set;}
        public TLRequestHttp_wait Response { get; set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            max_delay = br.ReadInt32();
wait_after = br.ReadInt32();
max_wait = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(max_delay);
bw.Write(wait_after);
bw.Write(max_wait);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLRequestHttp_wait)ObjectUtils.DeserializeObject(br);

		}
    }
}
