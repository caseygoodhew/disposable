using System;
using System.Collections.Generic;

namespace Disposable.MessagePipe
{
    public class Messanger<TMessageTypeEnum> : IMessanger<TMessageTypeEnum> where TMessageTypeEnum : struct, IConvertible
    {
        private readonly MessageContext<TMessageTypeEnum> messageContext;

        private readonly IEnumerator<Action<IMessanger<TMessageTypeEnum>, MessageContext<TMessageTypeEnum>>> handlersEnumerator;

        internal Messanger(IList<Action<IMessanger<TMessageTypeEnum>, MessageContext<TMessageTypeEnum>>> handlers, MessageContext<TMessageTypeEnum> messageContext)
        {
            if (!typeof(TMessageTypeEnum).IsEnum)
            {
                throw new ArgumentException("TMessageTypeEnum must be an enumerated type");
            }
            
            handlersEnumerator = handlers.GetEnumerator();
            this.messageContext = messageContext;
        }

        public void Forward()
        {
            var handler = GetNext();

            if (handler != null)
            {
                handler.Invoke(this, messageContext);
            }
        }

        private Action<IMessanger<TMessageTypeEnum>, MessageContext<TMessageTypeEnum>> GetNext()
        {
            return handlersEnumerator.MoveNext() ? handlersEnumerator.Current : null;
        }
    }
}
