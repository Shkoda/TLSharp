using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1933520591)]
    public class TLMsgs_all_info : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1933520591;
            }
        }

             public TLVector<long> msg_ids {get;set;}
     public byte[] info {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            msg_ids = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
info = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(msg_ids,bw);
BytesUtil.Serialize(info,bw);

        }
    }
}
