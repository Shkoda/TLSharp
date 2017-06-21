using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-204906213)]
    public class TLDecryptedMessageActionRequestKey : TLAbsDecryptedMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -204906213;
            }
        }

             public long exchange_id {get;set;}
     public byte[] g_a {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            exchange_id = br.ReadInt64();
g_a = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(exchange_id);
BytesUtil.Serialize(g_a,bw);

        }
    }
}
