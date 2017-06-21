using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(1877046107)]
    public class TLDecryptedMessageActionAcceptKey : TLAbsDecryptedMessageAction
    {
        public override int Constructor
        {
            get
            {
                return 1877046107;
            }
        }

             public long exchange_id {get;set;}
     public byte[] g_b {get;set;}
     public long key_fingerprint {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            exchange_id = br.ReadInt64();
g_b = BytesUtil.Deserialize(br);
key_fingerprint = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(exchange_id);
BytesUtil.Serialize(g_b,bw);
bw.Write(key_fingerprint);

        }
    }
}
