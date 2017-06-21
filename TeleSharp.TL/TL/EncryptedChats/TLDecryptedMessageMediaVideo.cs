using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(1380598109)]
    public class TLDecryptedMessageMediaVideo : TLAbsDecryptedMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return 1380598109;
            }
        }

             public byte[] thumb {get;set;}
     public int thumb_w {get;set;}
     public int thumb_h {get;set;}
     public int duration {get;set;}
     public string mime_type {get;set;}
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
duration = br.ReadInt32();
mime_type = StringUtil.Deserialize(br);
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
bw.Write(duration);
StringUtil.Serialize(mime_type,bw);
bw.Write(w);
bw.Write(h);
bw.Write(size);
BytesUtil.Serialize(key,bw);
BytesUtil.Serialize(iv,bw);

        }
    }
}
