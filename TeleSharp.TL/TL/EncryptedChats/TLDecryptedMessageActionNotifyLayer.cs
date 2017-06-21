using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-217806717)]
    public class TLDecryptedMessageActionNotifyLayer : TLAbsDecryptedMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -217806717;
            }
        }

             public int layer {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            layer = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(layer);

        }
    }
}
