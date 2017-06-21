using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-860719551)]
    public class TLDecryptedMessageActionTyping : TLAbsDecryptedMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -860719551;
            }
        }

             public TLAbsSendMessageAction action {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            action = (TLAbsSendMessageAction)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(action,bw);

        }
    }
}
