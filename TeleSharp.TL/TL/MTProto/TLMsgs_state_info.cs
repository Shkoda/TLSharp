using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(81704317)]
    public class TLMsgs_state_info : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 81704317;
            }
        }

             public long req_msg_id {get;set;}
     public byte[] info {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            req_msg_id = br.ReadInt64();
info = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(req_msg_id);
BytesUtil.Serialize(info,bw);

        }
    }
}
