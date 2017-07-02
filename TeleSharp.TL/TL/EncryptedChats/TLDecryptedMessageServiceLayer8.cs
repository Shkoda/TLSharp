using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1438109059)]
    public class TLDecryptedMessageServiceLayer8 : TLAbsDecryptedMessage
    {
        public override int Constructor
        {
            get
            {
                return -1438109059;
            }
        }

             public long random_id {get;set;}
        public byte[] random_bytes { get; set; }
        public TLAbsDecryptedMessageAction action {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            random_id = br.ReadInt64();
            random_bytes = BytesUtil.Deserialize(br);
            action = (TLAbsDecryptedMessageAction)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(random_id);
            BytesUtil.Serialize(random_bytes, bw);
            ObjectUtils.SerializeObject(action,bw);

        }
    }
}
