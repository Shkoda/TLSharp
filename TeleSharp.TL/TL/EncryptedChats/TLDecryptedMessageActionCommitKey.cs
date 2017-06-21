using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-332526693)]
    public class TLDecryptedMessageActionCommitKey : TLAbsDecryptedMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -332526693;
            }
        }

             public long exchange_id {get;set;}
     public long key_fingerprint {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            exchange_id = br.ReadInt64();
key_fingerprint = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(exchange_id);
bw.Write(key_fingerprint);

        }
    }
}
