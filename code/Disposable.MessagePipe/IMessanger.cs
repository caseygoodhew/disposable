using System;

namespace Disposable.MessagePipe
{
    public interface IMessanger<TMessageTypeEnum> where TMessageTypeEnum : struct, IConvertible
    {
        void Forward();
    }
}