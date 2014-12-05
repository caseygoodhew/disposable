using System;
using System.Collections.Generic;

namespace Disposable.MessagePipe
{
    public class Messanger<TMessageTypeEnum> : IMessanger<TMessageTypeEnum> where TMessageTypeEnum : struct, IConvertible
    {
        private readonly MessageContext<TMessageTypeEnum> messageContext;

        private readonly IEnumerator<Action<IMessanger<TMessageTypeEnum>>> handlersEnumerator;

        internal Messanger(IList<Action<IMessanger<TMessageTypeEnum>>> handlers, MessageContext<TMessageTypeEnum> messageContext)
        {
            if (!typeof(TMessageTypeEnum).IsEnum)
            {
                throw new ArgumentException("TMessageTypeEnum must be an enumerated type");
            }

            if (handlers == null)
            {
                throw new ArgumentNullException("handlers");
            }

            if (messageContext == null)
            {
                throw new ArgumentNullException("messageContext");
            }

            handlersEnumerator = handlers.GetEnumerator();
            this.messageContext = messageContext;
        }

        public MessageContext<TMessageTypeEnum> GetContext()
        {
            return messageContext;
        }
        
        public TMessageContext GetContext<TMessageContext>() where TMessageContext : MessageContext<TMessageTypeEnum>
        {
            return (TMessageContext)messageContext;
        }

        public void Forward()
        {
            var handler = GetNext();

            if (handler != null)
            {
                handler.Invoke(this);
            }
        }

        private Action<IMessanger<TMessageTypeEnum>> GetNext()
        {
            return handlersEnumerator.MoveNext() ? handlersEnumerator.Current : null;
        }
    }
}
