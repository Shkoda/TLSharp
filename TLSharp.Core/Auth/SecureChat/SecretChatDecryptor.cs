using System.IO;
using TeleSharp.TL;
using TLSharp.Core.MTProto.Crypto;
using TLSharp.Core.Utils;

namespace TLSharp.Core.Auth.SecureChat
{
    public class SecretChatDecryptor
    {
        public static TLObject Decrypt(byte[] message, byte[] sharedKey)
        {
            var payloadWithKeys = EncryptedPayloadWithKeys.Deserialize(message);

            var aesKeyData = Helpers.CalcKey(sharedKey, payloadWithKeys.MessageKey, true);

            var paddedDecryptedData = AES.DecryptAES(aesKeyData, payloadWithKeys.EncryptedPayload);
            var plainBytes = paddedDecryptedData.TrimWithLengthOnArrayStart();

            using (var messageStream = new MemoryStream(plainBytes))
            using (var messageReader = new BinaryReader(messageStream))
            {
                return ObjectUtils.DeserializeObject(messageReader) as TLObject;
            }
        }
    }
}