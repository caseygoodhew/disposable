using System;

namespace Disposable.MessagePipe
{
    /// <summary>
    /// The message pipe allows listeners to register for announcement of events of a specific enum value.
    /// </summary>
    /// <typeparam name="TMessageTypeEnum">The enum type that will be used to register for and announce against.</typeparam>
    public interface IMessagePipe<TMessageTypeEnum> where TMessageTypeEnum : struct, IConvertible
    {
        /// <summary>
        /// Gets the <see cref="MessengerType"/>
        /// </summary>
        MessengerType MessengerType { get; }
        
        /// <summary>
        /// Registers a listener for a given message type.
        /// </summary>
        /// <param name="messageType">The message type (enum value) to listen for.</param>
        /// <param name="handler">The action to be called when the message is announced.</param>
        void Register(TMessageTypeEnum messageType, Action<IMessenger<TMessageTypeEnum>> handler);
        
        /// <summary>
        /// Announces an event for a message of a given type. The message type is encapsulated in the <see cref="messageContext"/>.
        /// </summary>
        /// <typeparam name="TMessageContext">The message context type.</typeparam>
        /// <param name="messageContext">The message context to announce.</param>
        void Announce<TMessageContext>(TMessageContext messageContext) where TMessageContext : MessageContext<TMessageTypeEnum>;
    }
}