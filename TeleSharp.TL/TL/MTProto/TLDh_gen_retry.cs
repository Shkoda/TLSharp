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
	[TLObject(1188831161)]
    public class TLDh_gen_retry : TLAbsSet_client_DH_params_answer
    {
        public override int Constructor
        {
            get
            {
                return 1188831161;
            }
        }

             public Int128 nonce {get;set;}
     public Int128 server_nonce {get;set;}
     public Int128 new_nonce_hash2 {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            nonce = (Int128)ObjectUtils.DeserializeObject(br);
server_nonce = (Int128)ObjectUtils.DeserializeObject(br);
new_nonce_hash2 = (Int128)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(nonce,bw);
ObjectUtils.SerializeObject(server_nonce,bw);
ObjectUtils.SerializeObject(new_nonce_hash2,bw);

        }
    }
}
