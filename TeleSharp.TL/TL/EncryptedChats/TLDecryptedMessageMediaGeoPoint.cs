using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(893913689)]
    public class TLDecryptedMessageMediaGeoPoint : TLAbsDecryptedMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return 893913689;
            }
        }

             public double lat {get;set;}
     public double @long {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            lat = br.ReadDouble();
@long = br.ReadDouble();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(lat);
bw.Write(@long);

        }
    }
}
