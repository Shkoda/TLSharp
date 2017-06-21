using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(467867529)]
    public class TLDecryptedMessageLayer : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 467867529;
            }
        }

             public byte[] random_bytes {get;set;}
     public int layer {get;set;}
     public int in_seq_no {get;set;}
     public int out_seq_no {get;set;}
     public TLAbsDecryptedMessage message {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            random_bytes = BytesUtil.Deserialize(br);
layer = br.ReadInt32();
in_seq_no = br.ReadInt32();
out_seq_no = br.ReadInt32();
message = (TLAbsDecryptedMessage)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            BytesUtil.Serialize(random_bytes,bw);
bw.Write(layer);
bw.Write(in_seq_no);
bw.Write(out_seq_no);
ObjectUtils.SerializeObject(message,bw);

        }
    }
}
