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
	[TLObject(-1249309254)]
    public class TLServer_DH_inner_data : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1249309254;
            }
        }

             public Int128 nonce {get;set;}
     public Int128 server_nonce {get;set;}
     public int g {get;set;}
     public byte[] dh_prime {get;set;}
     public byte[] g_a {get;set;}
     public int server_time {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            nonce = (Int128)ObjectUtils.DeserializeObject(br);
server_nonce = (Int128)ObjectUtils.DeserializeObject(br);
g = br.ReadInt32();
dh_prime = BytesUtil.Deserialize(br);
g_a = BytesUtil.Deserialize(br);
server_time = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(nonce,bw);
ObjectUtils.SerializeObject(server_nonce,bw);
bw.Write(g);
BytesUtil.Serialize(dh_prime,bw);
BytesUtil.Serialize(g_a,bw);
bw.Write(server_time);

        }
    }
}
