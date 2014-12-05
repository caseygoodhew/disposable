using System;
using System.Collections.Generic;

namespace Disposable.MessagePipe
{
    /// <summary>
    /// The Stepping Messenger encapsulates the <see cref="MessageContext{T}"/> and controls 
    /// the delivery by forcing each listener to explicitly call the Forward method if the 
    /// announcement should be made to the next listener. 
    /// This allows each listener to block the messenger from making further announcements.
    /// </summary>
    /// <typeparam name="TMessageTypeEnum">The enum type being announced.</typeparam>
    public class SteppingMessenger<TMessageTypeEnum> : IMessenger<TMessageTypeEnum> where TMessageTypeEnum : struct, IConvertible
    {
        private readonly MessageContext<TMessageTypeEnum> messageContext;

        private readonly IEnumerator<Action<IMessenger<TMessageTypeEnum>>> handlersEnumerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SteppingMessenger{TMessageTypeEnum}"/> class.
        /// </summary>
        /// <param name="handlers">The handlers to announce to in the order the they should be called.</param>
        /// <param name="messageContext">The <see cref="MessageContext{TMessageTypeEnum}"/>.</param>
        internal SteppingMessenger(IList<Action<IMessenger<TMessageTypeEnum>>> handlers, MessageContext<TMessageTypeEnum> messageContext)
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

        /// <summary>
        /// Gets the uncast <see cref="MessageContext{TMessageTypeEnum}"/>.
        /// </summary>
        /// <returns>The <see cref="MessageContext{TMessageTypeEnum}"/>.</returns>
        public MessageContext<TMessageTypeEnum> GetContext()
        {
            return messageContext;
        }

        /// <summary>
        /// Gets the <see cref="MessageContext{TMessageTypeEnum}"/> cast to the given generic type.
        /// </summary>
        /// <typeparam name="TMessageContext">The type to cast the internal message context to.</typeparam>
        /// <returns>The <see cref="MessageContext{TMessageTypeEnum}"/> cast to the given generic type.</returns>
        public TMessageContext GetContext<TMessageContext>() where TMessageContext : MessageContext<TMessageTypeEnum>
        {
            return (TMessageContext)messageContext;
        }

        /// <summary>
        /// Instructs the messenger to forward the message to the next listener. 
        /// </summary>
        public void Forward()
        {
            var handler = GetNext();

            if (handler != null)
            {
                handler.Invoke(this);
            }
        }

        private Action<IMessenger<TMessageTypeEnum>> GetNext()
        {
            return handlersEnumerator.MoveNext() ? handlersEnumerator.Current : null;
        }
    }
}
