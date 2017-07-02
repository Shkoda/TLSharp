using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(880243653)]
    public class TLPong : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 880243653;
            }
        }

             public long msg_id {get;set;}
     public long ping_id {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            msg_id = br.ReadInt64();
ping_id = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(msg_id);
bw.Write(ping_id);

        }
    }
}
