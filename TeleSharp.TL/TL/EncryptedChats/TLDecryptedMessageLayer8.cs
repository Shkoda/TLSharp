using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(528568095)] 
    public class TLDecryptedMessageLayer8 : TLAbsDecryptedMessage
    {
        private static byte[] GenerateRandomBytes(int num)
        {
            byte[] data = new byte[num];
            new Random().NextBytes(data);
            return data;
        }

        public override int Constructor
        {
            get
            {
                return 528568095;
            }
        }

             public long random_id {get;set;}
        public byte[] random_bytes { get; set; }
     public string message {get;set;}
     public TLAbsDecryptedMessageMedia media {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            random_id = br.ReadInt64();
            random_bytes = BytesUtil.Deserialize(br);
            message = StringUtil.Deserialize(br);
media = (TLAbsDecryptedMessageMedia)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(random_id);
            BytesUtil.Serialize(random_bytes, bw);
            StringUtil.Serialize(message,bw);
                ObjectUtils.SerializeObject(media, bw);
        }
    }
}
