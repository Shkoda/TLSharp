using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-212046591)]
    public class TLRpc_result : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -212046591;
            }
        }

             public long req_msg_id {get;set;}
     public Object result {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            req_msg_id = br.ReadInt64();
result = (Object)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(req_msg_id);
ObjectUtils.SerializeObject(result,bw);

        }
    }
}
