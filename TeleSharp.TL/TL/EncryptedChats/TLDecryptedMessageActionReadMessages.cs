using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(206520510)]
    public class TLDecryptedMessageActionReadMessages : TLAbsDecryptedMessageAction
    {
        public override int Constructor
        {
            get
            {
                return 206520510;
            }
        }

             public TLVector<long> random_ids {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            random_ids = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(random_ids,bw);

        }
    }
}
