using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(846826124)]
    public class TLDecryptedMessageMediaPhoto : TLAbsDecryptedMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return 846826124;
            }
        }

             public byte[] thumb {get;set;}
     public int thumb_w {get;set;}
     public int thumb_h {get;set;}
     public int w {get;set;}
     public int h {get;set;}
     public int size {get;set;}
     public byte[] key {get;set;}
     public byte[] iv {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            thumb = BytesUtil.Deserialize(br);
thumb_w = br.ReadInt32();
thumb_h = br.ReadInt32();
w = br.ReadInt32();
h = br.ReadInt32();
size = br.ReadInt32();
key = BytesUtil.Deserialize(br);
iv = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            BytesUtil.Serialize(thumb,bw);
bw.Write(thumb_w);
bw.Write(thumb_h);
bw.Write(w);
bw.Write(h);
bw.Write(size);
BytesUtil.Serialize(key,bw);
BytesUtil.Serialize(iv,bw);

        }
    }
}
