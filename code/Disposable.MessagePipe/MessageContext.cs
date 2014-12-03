using System;
using System.Collections.Generic;

namespace Disposable.MessagePipe
{
    public class MessageContext<TMessageTypeEnum>
    {
        public readonly TMessageTypeEnum MessageType;

        public readonly IDictionary<string, object> Dictionary = new Dictionary<string, object>();
        
        public MessageContext(TMessageTypeEnum messageType)
        {
            if (!typeof(TMessageTypeEnum).IsEnum)
            {
                throw new ArgumentException("TMessageTypeEnum must be an enumerated type");
            } 
            
            MessageType = messageType;
        }
    }
}