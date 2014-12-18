using System;
using System.Collections.Generic;

namespace Disposable.MessagePipe
{
    /// <summary>
    /// The message context used in a message announcement
    /// </summary>
    /// <typeparam name="TMessageTypeEnum">The message type enum</typeparam>
    public class MessageContext<TMessageTypeEnum>
    {
        /// <summary>
        /// The message type
        /// </summary>
        public readonly TMessageTypeEnum MessageType;

        /// <summary>
        /// An open dictionary that can be used by any message listener to store and forward data to listeners further down the chain.
        /// </summary>
        public readonly IDictionary<string, object> Dictionary = new Dictionary<string, object>();
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageContext{TMessageTypeEnum}"/> class.
        /// </summary>
        /// <param name="messageType">The message type</param>
        public MessageContext(TMessageTypeEnum messageType)
        {
            if (!typeof(TMessageTypeEnum).IsEnum)
            {
                throw new ArgumentException("TMessageTypeEnum must be an enumerated type");
            }

            MessageType = messageType;
        }

        /// <summary>
        /// Verifies that the <see cref="MessageType"/> is the <see cref="expectedMessageType"/>.
        /// </summary>
        /// <param name="expectedMessageType"></param>
        /// <returns></returns>
        public bool Is(TMessageTypeEnum expectedMessageType)
        {
            return MessageType.Equals(expectedMessageType);
        }
    }
}