using System;
using System.Collections.Generic;
using System.Linq;

namespace Disposable.MessagePipe
{
    /// <summary>
    /// The message pipe allows listeners to register for announcement of events of a specific enum value.
    /// </summary>
    /// <typeparam name="TMessageTypeEnum">The enum type that will be used to register for and announce against</typeparam>
    public class MessagePipe<TMessageTypeEnum> : IMessagePipe<TMessageTypeEnum> where TMessageTypeEnum : struct, IConvertible
    {
        private readonly Dictionary<TMessageTypeEnum, List<Action<IMessenger<TMessageTypeEnum>>>> lookup =
            new Dictionary<TMessageTypeEnum, List<Action<IMessenger<TMessageTypeEnum>>>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagePipe{TMessageTypeEnum}"/> class.
        /// </summary>
        /// <param name="messengerType">The <see cref="MessengerType"/> to use for this pipe.</param>
        public MessagePipe(MessengerType messengerType)
        {
            if (!typeof(TMessageTypeEnum).IsEnum) 
            {
                throw new ArgumentException("TMessageTypeEnum must be an enumerated type");
            }

            MessengerType = messengerType;
        }

        /// <summary>
        /// Gets the <see cref="IMessagePipe{TMessageTypeEnum}.MessengerType"/>
        /// </summary>
        public MessengerType MessengerType { get; private set; }

        /// <summary>
        /// Registers a listener for a given message type.
        /// </summary>
        /// <param name="messageType">The message type (enum value) to listen for.</param>
        /// <param name="handler">The action to be called when the message is announced.</param>
        public void Register(TMessageTypeEnum messageType, Action<IMessenger<TMessageTypeEnum>> handler)
        {
            var list = GetListFor(messageType);
            list.Add(handler);
        }

        /// <summary>
        /// Announces an event for a message of a given type. The message type is encapsulated in the <see cref="messageContext"/>.
        /// </summary>
        /// <typeparam name="TMessageContext">The message context type.</typeparam>
        /// <param name="messageContext">The message context to announce.</param>
        public void Announce<TMessageContext>(TMessageContext messageContext)
            where TMessageContext : MessageContext<TMessageTypeEnum>
        {
            var list = GetListFor(messageContext.MessageType);

            if (!list.Any())
            {
                return;
            }

            CreateMessenger(list, messageContext).Forward();
        }

        private List<Action<IMessenger<TMessageTypeEnum>>> GetListFor(TMessageTypeEnum messageType)
        {
            List<Action<IMessenger<TMessageTypeEnum>>> list;

            if (!lookup.TryGetValue(messageType, out list))
            {
                list = new List<Action<IMessenger<TMessageTypeEnum>>>();
                lookup[messageType] = list;
            }

            return list;
        }

        private IMessenger<TMessageTypeEnum> CreateMessenger<TMessageContext>(
            IList<Action<IMessenger<TMessageTypeEnum>>> list,
            TMessageContext messageContext) where TMessageContext : MessageContext<TMessageTypeEnum>
        {
            switch (MessengerType)
            {
                case MessengerType.Stepping:
                    return new SteppingMessenger<TMessageTypeEnum>(list, messageContext);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}