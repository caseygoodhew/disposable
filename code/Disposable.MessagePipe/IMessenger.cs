using System;

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
        /// <returns>The <see cref="MessageContext{TMessageTypeEnum}"/>.</returns>
        MessageContext<TMessageTypeEnum> GetContext();
        
        /// <summary>
        /// Gets the <see cref="MessageContext{TMessageTypeEnum}"/> cast to the given generic type.
        /// </summary>
        /// <typeparam name="TMessageContext">The type to cast the internal message context to.</typeparam>
        /// <returns>The <see cref="MessageContext{TMessageTypeEnum}"/> cast to the given generic type.</returns>
        TMessageContext GetContext<TMessageContext>() where TMessageContext : MessageContext<TMessageTypeEnum>;
        
        /// <summary>
        /// Instructs the messenger to forward the message to the next listener. 
        /// </summary>
        void Forward();
    }
}