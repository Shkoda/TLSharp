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
	[TLObject(1715713620)]
    public class TLClient_DH_inner_data : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1715713620;
            }
        }

             public Int128 nonce {get;set;}
     public Int128 server_nonce {get;set;}
     public long retry_id {get;set;}
     public byte[] g_b {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            nonce = (Int128)ObjectUtils.DeserializeObject(br);
server_nonce = (Int128)ObjectUtils.DeserializeObject(br);
retry_id = br.ReadInt64();
g_b = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(nonce,bw);
ObjectUtils.SerializeObject(server_nonce,bw);
bw.Write(retry_id);
BytesUtil.Serialize(g_b,bw);

        }
    }
}
