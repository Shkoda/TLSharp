using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(1474341323)]
    public class TLDecryptedMessageMediaAudio : TLAbsDecryptedMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return 1474341323;
            }
        }

             public int duration {get;set;}
     public string mime_type {get;set;}
     public int size {get;set;}
     public byte[] key {get;set;}
     public byte[] iv {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            duration = br.ReadInt32();
mime_type = StringUtil.Deserialize(br);
size = br.ReadInt32();
key = BytesUtil.Deserialize(br);
iv = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(duration);
StringUtil.Serialize(mime_type,bw);
bw.Write(size);
BytesUtil.Serialize(key,bw);
BytesUtil.Serialize(iv,bw);

        }
    }
}
