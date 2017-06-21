using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(1930838368)]
    public class TLDecryptedMessageService : TLAbsDecryptedMessage
    {
        public override int Constructor
        {
            get
            {
                return 1930838368;
            }
        }

             public long random_id {get;set;}
     public TLAbsDecryptedMessageAction action {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            random_id = br.ReadInt64();
action = (TLAbsDecryptedMessageAction)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(random_id);
ObjectUtils.SerializeObject(action,bw);

        }
    }
}
