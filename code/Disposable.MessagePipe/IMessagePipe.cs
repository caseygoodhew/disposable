using System;

namespace Disposable.MessagePipe
{
    public interface IMessagePipe<TMessageTypeEnum> where TMessageTypeEnum : struct, IConvertible
    {
        void Register(TMessageTypeEnum messageType, Action<IMessanger<TMessageTypeEnum>, MessageContext<TMessageTypeEnum>> handler);
        
        void Announce<TMessageContext>(TMessageContext messageContext) where TMessageContext : MessageContext<TMessageTypeEnum>;
    }
}