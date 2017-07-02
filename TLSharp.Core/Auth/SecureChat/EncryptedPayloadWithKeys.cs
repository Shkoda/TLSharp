using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
using TLSharp.Core.MTProto.Crypto;
using TLSharp.Core.Utils;

namespace TLSharp.Core.Auth.SecureChat
{
    public class EncryptedPayloadWithKeys
    {
        public byte[] EncryptedPayload { get; }
        public byte[] MessageKey { get; }
        public byte[] SharedKeyFingetprint { get; }

        public EncryptedPayloadWithKeys(byte[] encryptedPayload, byte[] messageKey, byte[] sharedKeyFingetprint)
        {
            EncryptedPayload = encryptedPayload;
            MessageKey = messageKey;
            SharedKeyFingetprint = sharedKeyFingetprint;
        }

        public static EncryptedPayloadWithKeys Deserialize(byte[] bytes)
        {
            EncryptedPayloadWithKeys deserialized;
            using (var stream = new MemoryStream(bytes))
            using (var reader = new BinaryReader(stream))
            {
                var keyFingerprint = reader.ReadBytes(8);
                var msgKey = reader.ReadBytes(16);
                var encryptedBytes = reader.ReadBytes(bytes.Length - 8 - 16);
            
                deserialized = new EncryptedPayloadWithKeys(encryptedBytes,msgKey, keyFingerprint);
            }
            return deserialized;
        }

        public  byte[] Serialize()
        {
            var length = MessageKey.Length + SharedKeyFingetprint.Length + EncryptedPayload.Length;
            byte[] serialized;
            using (var ciphertextPacket = new MemoryStream(new byte[length], 0, length, true, true))
            {
                using (var writer = new BinaryWriter(ciphertextPacket))
                {
                    writer.Write(SharedKeyFingetprint);
                    writer.Write(MessageKey);
                    writer.Write(EncryptedPayload);

                    serialized = (ciphertextPacket.ToArray());
                }
            }
            return serialized;
        }
    }
}
