using System;
using System.Collections.Generic;

namespace Disposable.MessagePipe
{
    /// <summary>
    /// The Stepping Messenger encapsulates the <see cref="MessageContext{TMessageTypeEnum}"/> and controls 
    /// the delivery by forcing each listener to explicitly call the Forward method if the 
    /// announcement should be made to the next listener. 
    /// This allows each listener to block the messenger from making further announcements.
    /// </summary>
    /// <typeparam name="TMessageTypeEnum">The enum type being announced.</typeparam>
    public class SteppingMessenger<TMessageTypeEnum> : Messenger<TMessageTypeEnum> where TMessageTypeEnum : struct, IConvertible
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SteppingMessenger{TMessageTypeEnum}"/> class.
        /// </summary>
        /// <param name="handlers">The handlers to announce to in the order the they should be called.</param>
        /// <param name="messageContext">The <see cref="MessageContext{TMessageTypeEnum}"/>.</param>
        public SteppingMessenger(IList<Action<IMessenger<TMessageTypeEnum>>> handlers, MessageContext<TMessageTypeEnum> messageContext)
            : base(handlers, messageContext)
        {
        }
    }
}
