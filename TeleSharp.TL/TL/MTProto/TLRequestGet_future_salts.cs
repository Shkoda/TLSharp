using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1188971260)]
    public class TLRequestGet_future_salts : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1188971260;
            }
        }

                public int num {get;set;}
        public TLFuture_salts Response{ get; set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            num = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(num);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (TLFuture_salts)ObjectUtils.DeserializeObject(br);

		}
    }
}
