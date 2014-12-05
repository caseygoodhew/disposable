using System;

namespace Disposable.MessagePipe
{
    public interface IMessanger<TMessageTypeEnum> where TMessageTypeEnum : struct, IConvertible
    {
        MessageContext<TMessageTypeEnum> GetContext();
        
        TMessageContext GetContext<TMessageContext>() where TMessageContext : MessageContext<TMessageTypeEnum>;
        
        void Forward();
    }
}