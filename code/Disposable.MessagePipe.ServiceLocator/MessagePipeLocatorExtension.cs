using System;

using Disposable.Common.ServiceLocator;

namespace Disposable.MessagePipe.ServiceLocator
{
    /// <summary>
    /// Allows message registration to service located instances.
    /// </summary>
    public static class MessagePipeLocatorExtension
    {
        /// <summary>
        /// Allows message registration to service located instances.
        /// </summary>
        /// <typeparam name="TMessageTypeEnum">The message type enum.</typeparam>
        /// <typeparam name="TClass">The instance to be pulled from the locator.</typeparam>
        /// <param name="messagePipe">The message pipe instance.</param>
        /// <param name="messageType">The message type (enum value) to listen for.</param>
        /// <param name="handler">The handler method of the located instance to invoke on message announcement.</param>
        public static void Locator<TMessageTypeEnum, TClass>(
            this MessagePipe<TMessageTypeEnum> messagePipe,
            TMessageTypeEnum messageType,
            Func<TClass, Action<IMessenger<TMessageTypeEnum>>> handler)
            where TMessageTypeEnum : struct, IConvertible
            where TClass : class
        {
            messagePipe.Register(
                messageType,
                messenger =>
                    {
                        var instance = Common.ServiceLocator.Locator.Current.Instance<TClass>();
                        var boundAction = handler.Invoke(instance);
                        boundAction.Invoke(messenger);
                    });
        }
    }
}
