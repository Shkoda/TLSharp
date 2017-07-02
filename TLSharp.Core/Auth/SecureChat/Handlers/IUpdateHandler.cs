using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;

namespace TLSharp.Core.Auth.SecureChat.Handlers
{
    public interface IUpdateHandler<T> where T : TLAbsUpdate
    {
        void Handle(T t);
    }
}
