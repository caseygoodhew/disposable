using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Disposable.MessagePipe
{
    /// <summary>
    /// A messenger encapsulates the <see cref="MessageContext{T}"/> and controls the delivery scheme.
    /// </summary>
    /// <typeparam name="TMessageTypeEnum">The enum type being announced.</typeparam>
    public interface IMessenger<TMessageTypeEnum> where TMessageTypeEnum : struct, IConvertible
    {
        /// <summary>
        /// Gets the uncast <see cref="MessageContext{TMessageTypeEnum}"/>.
        /// </summary>
        /// <param name="expectedMessageType">Expected this or any other message type in the <see cref="orExpectedMessageTypes"/>.</param>
        /// <param name="orExpectedMessageTypes">Expected <see cref="expectedMessageType"/> or an other of these message types.</param>
        /// <returns>The <see cref="MessageContext{TMessageTypeEnum}"/>The <see cref="MessageContext{TMessageTypeEnum}"/>.</returns>
        /// <remarks>If the message type in the <see cref="MessageContext{TMessageTypeEnum"/> does not match one of the expected message types, an <see cref="InvalidOperationException"/> must be thrown.</remarks>
        MessageContext<TMessageTypeEnum> GetContext(TMessageTypeEnum expectedMessageType, params TMessageTypeEnum[] orExpectedMessageTypes);
            
        /// <summary>
        /// Gets the uncast <see cref="MessageContext{TMessageTypeEnum}"/>.
        /// </summary>
        /// <param name="expectedMessageTypes">The message context message type must be one of the expected these message types.</param>
        /// <returns>The <see cref="MessageContext{TMessageTypeEnum}"/>The <see cref="MessageContext{TMessageTypeEnum}"/>.</returns>
        /// <remarks>If the message type in the <see cref="MessageContext{TMessageTypeEnum"/> does not match one of the expected message types, an <see cref="InvalidOperationException"/> must be thrown.</remarks>
        MessageContext<TMessageTypeEnum> GetContext(IEnumerable<TMessageTypeEnum> expectedMessageTypes);

        /// <summary>
        /// Gets the <see cref="MessageContext{TMessageTypeEnum}"/> cast to the given generic type.
        /// </summary>
        /// <typeparam name="TMessageContext">The type to cast the internal message context to.</typeparam>
        /// <param name="expectedMessageType">Expected this or any other message type in the <see cref="orExpectedMessageTypes"/>.</param>
        /// <param name="orExpectedMessageTypes">Expected <see cref="expectedMessageType"/> or an other of these message types.</param>
        /// <returns>The <see cref="MessageContext{TMessageTypeEnum}"/> cast to the given generic type.</returns>
        /// <remarks>If the message type in the <see cref="MessageContext{TMessageTypeEnum"/> does not match one of the expected message types, an <see cref="InvalidOperationException"/> must be thrown.</remarks>
        TMessageContext GetContext<TMessageContext>(TMessageTypeEnum expectedMessageType, params TMessageTypeEnum[] orExpectedMessageTypes) where TMessageContext : MessageContext<TMessageTypeEnum>;

        /// <summary>
        /// Gets the <see cref="MessageContext{TMessageTypeEnum}"/> cast to the given generic type.
        /// </summary>
        /// <typeparam name="TMessageContext">The type to cast the internal message context to.</typeparam>
        /// <param name="expectedMessageTypes">The message context message type must be one of the expected these message types.</param>
        /// <returns>The <see cref="MessageContext{TMessageTypeEnum}"/> cast to the given generic type.</returns>
        /// <remarks>If the message type in the <see cref="MessageContext{TMessageTypeEnum"/> does not match one of the expected message types, an <see cref="InvalidOperationException"/> must be thrown.</remarks>
        TMessageContext GetContext<TMessageContext>(IEnumerable<TMessageTypeEnum> expectedMessageTypes) where TMessageContext : MessageContext<TMessageTypeEnum>;

        /// <summary>
        /// Instructs the messenger to forward the message to the next listener. 
        /// </summary>
        void Forward();
    }
}