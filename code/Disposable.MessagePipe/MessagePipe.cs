using System;
using System.Collections.Generic;
using System.Linq;

namespace Disposable.MessagePipe
{
    public class MessagePipe<TMessageTypeEnum> : IMessagePipe<TMessageTypeEnum> where TMessageTypeEnum : struct, IConvertible
    {
        private readonly
            Dictionary<TMessageTypeEnum, List<Action<IMessanger<TMessageTypeEnum>, MessageContext<TMessageTypeEnum>>>>
            lookup =
                new Dictionary
                    <TMessageTypeEnum, List<Action<IMessanger<TMessageTypeEnum>, MessageContext<TMessageTypeEnum>>>>();
        
        public MessagePipe()
        {
            if (!typeof(TMessageTypeEnum).IsEnum) 
            {
                throw new ArgumentException("TMessageTypeEnum must be an enumerated type");
            }
        }

        public void Register(TMessageTypeEnum messageType, Action<IMessanger<TMessageTypeEnum>, MessageContext<TMessageTypeEnum>> handler)
        {
            var list = GetListFor(messageType);
            list.Add(handler);
        }

        public void Announce<TMessageContext>(TMessageContext messageContext) where TMessageContext : MessageContext<TMessageTypeEnum>
        {
            var list = GetListFor(messageContext.MessageType);
            
            if (!list.Any())
            {
                return;
            }

            var messanger = new Messanger<TMessageTypeEnum>(list, messageContext);
            messanger.Forward();
        }

        private List<Action<IMessanger<TMessageTypeEnum>, MessageContext<TMessageTypeEnum>>> GetListFor(
            TMessageTypeEnum messageType)
        {
            List<Action<IMessanger<TMessageTypeEnum>, MessageContext<TMessageTypeEnum>>> list;

            if (!lookup.TryGetValue(messageType, out list))
            {
                list = new List<Action<IMessanger<TMessageTypeEnum>, MessageContext<TMessageTypeEnum>>>();
                lookup[messageType] = list;
            }

            return list;
        }
    }
}