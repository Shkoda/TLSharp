using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(541931640)]
    public class TLDecryptedMessage : TLAbsDecryptedMessage
    {
        public override int Constructor
        {
            get
            {
                return 541931640;
            }
        }

             public long random_id {get;set;}
     public int ttl {get;set;}
     public string message {get;set;}
     public TLAbsDecryptedMessageMedia media {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            random_id = br.ReadInt64();
ttl = br.ReadInt32();
message = StringUtil.Deserialize(br);
media = (TLAbsDecryptedMessageMedia)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(random_id);
bw.Write(ttl);
StringUtil.Serialize(message,bw);
            if (media != null)
            {
                ObjectUtils.SerializeObject(media, bw);
            }

        }
    }
}
