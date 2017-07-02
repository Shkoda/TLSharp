using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigMath;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-2083955988)]
    public class TLP_q_inner_data : TLAbsP_Q_inner_data
    {
        public override int Constructor
        {
            get
            {
                return -2083955988;
            }
        }

             public byte[] pq {get;set;}
     public byte[] p {get;set;}
     public byte[] q {get;set;}
     public Int128 nonce {get;set;}
     public Int128 server_nonce {get;set;}
     public Int256 new_nonce {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            pq = BytesUtil.Deserialize(br);
p = BytesUtil.Deserialize(br);
q = BytesUtil.Deserialize(br);
nonce = (Int128)ObjectUtils.DeserializeObject(br);
server_nonce = (Int128)ObjectUtils.DeserializeObject(br);
new_nonce = (Int256)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            BytesUtil.Serialize(pq,bw);
BytesUtil.Serialize(p,bw);
BytesUtil.Serialize(q,bw);
ObjectUtils.SerializeObject(nonce,bw);
ObjectUtils.SerializeObject(server_nonce,bw);
ObjectUtils.SerializeObject(new_nonce,bw);

        }
    }
}
