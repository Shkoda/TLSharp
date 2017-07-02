using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(1973679973)]
    public class TLBind_auth_key_inner : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1973679973;
            }
        }

             public long nonce {get;set;}
     public long temp_auth_key_id {get;set;}
     public long perm_auth_key_id {get;set;}
     public long temp_session_id {get;set;}
     public int expires_at {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            nonce = br.ReadInt64();
temp_auth_key_id = br.ReadInt64();
perm_auth_key_id = br.ReadInt64();
temp_session_id = br.ReadInt64();
expires_at = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(nonce);
bw.Write(temp_auth_key_id);
bw.Write(perm_auth_key_id);
bw.Write(temp_session_id);
bw.Write(expires_at);

        }
    }
}
