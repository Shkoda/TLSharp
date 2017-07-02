using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core.MTProto.Crypto;
using TLSharp.Core.Utils;

namespace TLSharp.Core.Auth.SecureChat
{
    public class EncryptedRequestBuilder
    {
        public static TLRequestSendEncrypted CreateSendEncryptedMessageRequest(string text, TLEncryptedChat acceptedChat, AuthKey authKey)
        {
            var textMessageId = Helpers.GenerateRandomLong();
            var plainMessage = CreateTextMessage(textMessageId, text);
            var plainBytesAndLength = plainMessage.Serialize().WithLengthOnArrayStart();

            var msgKey = Helpers.CalcMsgKey(plainBytesAndLength);
            var aesKeyData = Helpers.CalcKey(authKey.Data, msgKey, true);

            var paddedPlainBytes = plainBytesAndLength.WithAligment(16);
            var encryptedBytes = AES.EncryptAES(aesKeyData, paddedPlainBytes);

            var chipherBytesWithKeys =
                new EncryptedPayloadWithKeys(encryptedBytes, msgKey, authKey.FingerprintBytes).Serialize();

            var sendEncryptedMessageRequest = CreateRequestSendEncrypted(chipherBytesWithKeys, acceptedChat,
                textMessageId);
            return sendEncryptedMessageRequest;
        }


        private static TLRequestSendEncrypted CreateRequestSendEncrypted(byte[] chipherBytesWithKeys,
            TLEncryptedChat acceptedChat, long textMessageId)
        {
            var sendEncryptedMessageRequest = new TLRequestSendEncrypted
            {
                data = chipherBytesWithKeys,
                peer = new TLInputEncryptedChat
                {
                    access_hash = acceptedChat.access_hash,
                    chat_id = acceptedChat.id
                },
                random_id = textMessageId
            };
            return sendEncryptedMessageRequest;
        }

        private static TLDecryptedMessageLayer8 CreateTextMessage(long randomId, string text)
            => new TLDecryptedMessageLayer8
            {
                random_id = randomId,
                message = text,
                media = new TLDecryptedMessageMediaEmpty(),
                random_bytes = Helpers.GenerateRandomBytes(15)
            };
    }
}