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
	[TLObject(-790100132)]
    public class TLServer_DH_params_ok : TLAbsServer_DH_Params
    {
        public override int Constructor
        {
            get
            {
                return -790100132;
            }
        }

             public Int128 nonce {get;set;}
     public Int128 server_nonce {get;set;}
     public byte[] encrypted_answer {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            nonce = (Int128)ObjectUtils.DeserializeObject(br);
server_nonce = (Int128)ObjectUtils.DeserializeObject(br);
encrypted_answer = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(nonce,bw);
ObjectUtils.SerializeObject(server_nonce,bw);
BytesUtil.Serialize(encrypted_answer,bw);

        }
    }
}
