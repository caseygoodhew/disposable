using System;
using System.Collections.Generic;
using System.Linq;

namespace Disposable.MessagePipe
{
    /// <summary>
    /// The Messenger encapsulates the <see cref="MessageContext{T}"/> and implements a base 
    /// framework for specific implementations to exact their own delivery systems.
    /// </summary>
    /// <typeparam name="TMessageTypeEnum">The enum type being announced.</typeparam>
    public abstract class Messenger<TMessageTypeEnum> : IMessenger<TMessageTypeEnum>
        where TMessageTypeEnum : struct, IConvertible
    {
        private readonly MessageContext<TMessageTypeEnum> messageContext;

        private readonly IEnumerator<Action<IMessenger<TMessageTypeEnum>>> handlersEnumerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Messenger{TMessageTypeEnum}"/> class.
        /// </summary>
        /// <param name="handlers">The handlers to announce to in the order the they should be called.</param>
        /// <param name="messageContext">The <see cref="MessageContext{TMessageTypeEnum}"/>.</param>
        protected Messenger(IList<Action<IMessenger<TMessageTypeEnum>>> handlers, MessageContext<TMessageTypeEnum> messageContext)
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
        /// <param name="expectedMessageType">Expected this or any other message type in the <see cref="orExpectedMessageTypes"/>.</param>
        /// <param name="orExpectedMessageTypes">Expected <see cref="expectedMessageType"/> or an other of these message types.</param>
        /// <returns>The <see cref="MessageContext{TMessageTypeEnum}"/>The <see cref="MessageContext{TMessageTypeEnum}"/>.</returns>
        /// <remarks>If the message type in the <see cref="MessageContext{TMessageTypeEnum"/> does not match one of the expected message types, an <see cref="InvalidOperationException"/> must be thrown.</remarks>
        public MessageContext<TMessageTypeEnum> GetContext(TMessageTypeEnum expectedMessageType, params TMessageTypeEnum[] orExpectedMessageTypes)
        {
            return GetContext(new[] { expectedMessageType }.Concat(orExpectedMessageTypes));
        }

        /// <summary>
        /// Gets the uncast <see cref="MessageContext{TMessageTypeEnum}"/>.
        /// </summary>
        /// <param name="expectedMessageTypes">The message context message type must be one of the expected these message types.</param>
        /// <returns>The <see cref="MessageContext{TMessageTypeEnum}"/>The <see cref="MessageContext{TMessageTypeEnum}"/>.</returns>
        /// <remarks>If the message type in the <see cref="MessageContext{TMessageTypeEnum"/> does not match one of the expected message types, an <see cref="InvalidOperationException"/> must be thrown.</remarks>
        public MessageContext<TMessageTypeEnum> GetContext(IEnumerable<TMessageTypeEnum> expectedMessageTypes)
        {
            ValidateExpectedMessageTypes(expectedMessageTypes);

            return messageContext;
        }

        /// <summary>
        /// Gets the <see cref="MessageContext{TMessageTypeEnum}"/> cast to the given generic type.
        /// </summary>
        /// <typeparam name="TMessageContext">The type to cast the internal message context to.</typeparam>
        /// <param name="expectedMessageType">Expected this or any other message type in the <see cref="orExpectedMessageTypes"/>.</param>
        /// <param name="orExpectedMessageTypes">Expected <see cref="expectedMessageType"/> or an other of these message types.</param>
        /// <returns>The <see cref="MessageContext{TMessageTypeEnum}"/> cast to the given generic type.</returns>
        /// <remarks>If the message type in the <see cref="MessageContext{TMessageTypeEnum"/> does not match one of the expected message types, an <see cref="InvalidOperationException"/> must be thrown.</remarks>
        public TMessageContext GetContext<TMessageContext>(
            TMessageTypeEnum expectedMessageType,
            params TMessageTypeEnum[] orExpectedMessageTypes) where TMessageContext : MessageContext<TMessageTypeEnum>
        {
            return (TMessageContext)GetContext(expectedMessageType, orExpectedMessageTypes);
        }

        /// <summary>
        /// Gets the <see cref="MessageContext{TMessageTypeEnum}"/> cast to the given generic type.
        /// </summary>
        /// <typeparam name="TMessageContext">The type to cast the internal message context to.</typeparam>
        /// <param name="expectedMessageTypes">The message context message type must be one of the expected these message types.</param>
        /// <returns>The <see cref="MessageContext{TMessageTypeEnum}"/> cast to the given generic type.</returns>
        /// <remarks>If the message type in the <see cref="MessageContext{TMessageTypeEnum"/> does not match one of the expected message types, an <see cref="InvalidOperationException"/> must be thrown.</remarks>
        public TMessageContext GetContext<TMessageContext>(IEnumerable<TMessageTypeEnum> expectedMessageTypes) where TMessageContext : MessageContext<TMessageTypeEnum>
        {
            return (TMessageContext)GetContext(expectedMessageTypes);
        }

        /// <summary>
        /// Instructs the messenger to forward the message to the next listener. 
        /// </summary>
        public virtual void Forward()
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

        private void ValidateExpectedMessageTypes(IEnumerable<TMessageTypeEnum> expectedMessageTypes)
        {
            if (!expectedMessageTypes.Any(messageContext.Is))
            {
                throw new InvalidOperationException(string.Format("Unexpected message type {0}", messageContext.MessageType));
            }
        }
    }
}