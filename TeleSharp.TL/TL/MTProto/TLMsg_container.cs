using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(1945237724)]
    public class TLMsg_container : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1945237724;
            }
        }

             public TLVector<TLMessage> messages {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            messages = ObjectUtils.DeserializeVector<TLMessage>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(messages,bw);

        }
    }
}
