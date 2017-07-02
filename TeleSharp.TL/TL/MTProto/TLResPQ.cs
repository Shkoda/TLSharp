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
	[TLObject(85337187)]
    public class TLResPQ : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 85337187;
            }
        }

             public Int128 nonce {get;set;}
     public Int128 server_nonce {get;set;}
     public byte[] pq {get;set;}
     public TLVector<long> server_public_key_fingerprints {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            nonce = (Int128)ObjectUtils.DeserializeObject(br);
server_nonce = (Int128)ObjectUtils.DeserializeObject(br);
pq = BytesUtil.Deserialize(br);
server_public_key_fingerprints = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(nonce,bw);
ObjectUtils.SerializeObject(server_nonce,bw);
BytesUtil.Serialize(pq,bw);
ObjectUtils.SerializeObject(server_public_key_fingerprints,bw);

        }
    }
}
