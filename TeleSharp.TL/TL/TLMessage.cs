using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(1538843921)]
    public class TLMessage : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1538843921;
            }
        }

             public long msg_id {get;set;}
     public int seqno {get;set;}
     public int bytes {get;set;}
     public Object body {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            msg_id = br.ReadInt64();
seqno = br.ReadInt32();
bytes = br.ReadInt32();
body = (Object)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(msg_id);
bw.Write(seqno);
bw.Write(bytes);
ObjectUtils.SerializeObject(body,bw);

        }
    }
}
