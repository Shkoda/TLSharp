using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1586283796)]
    public class TLDecryptedMessageActionSetMessageTTL : TLAbsDecryptedMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -1586283796;
            }
        }

             public int ttl_seconds {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            ttl_seconds = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(ttl_seconds);

        }
    }
}
