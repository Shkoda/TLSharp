using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(1360072880)]
    public class TLDecryptedMessageActionResend : TLAbsDecryptedMessageAction
    {
        public override int Constructor
        {
            get
            {
                return 1360072880;
            }
        }

             public int start_seq_no {get;set;}
     public int end_seq_no {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            start_seq_no = br.ReadInt32();
end_seq_no = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(start_seq_no);
bw.Write(end_seq_no);

        }
    }
}
