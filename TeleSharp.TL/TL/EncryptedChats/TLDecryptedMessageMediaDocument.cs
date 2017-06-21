using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1332395189)]
    public class TLDecryptedMessageMediaDocument : TLAbsDecryptedMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return -1332395189;
            }
        }

             public byte[] thumb {get;set;}
     public int thumb_w {get;set;}
     public int thumb_h {get;set;}
     public string file_name {get;set;}
     public string mime_type {get;set;}
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
file_name = StringUtil.Deserialize(br);
mime_type = StringUtil.Deserialize(br);
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
StringUtil.Serialize(file_name,bw);
StringUtil.Serialize(mime_type,bw);
bw.Write(size);
BytesUtil.Serialize(key,bw);
BytesUtil.Serialize(iv,bw);

        }
    }
}
