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
            this IMessagePipe<TMessageTypeEnum> messagePipe,
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

        /// <summary>
        /// Creates, registers and returns a new <see cref="MessagePipe{TMessageTypeEnum}"/>.
        /// </summary>
        /// <typeparam name="TMessageTypeEnum">The message type.</typeparam>
        /// <param name="registrar">The <see cref="IRegistrar"/>.</param>
        /// <param name="messengerType">The <see cref="MessengerType"/>.</param>
        /// <param name="initiatorAction">(Optional) This method will be called with the newly created message pipe so that listeners can be registered.</param>
        /// <returns>The newly created message pipe.</returns>
        public static IMessagePipe<TMessageTypeEnum> CreatePipe<TMessageTypeEnum>(this IRegistrar registrar, MessengerType messengerType, Action<IMessagePipe<TMessageTypeEnum>> initiatorAction = null) where TMessageTypeEnum : struct, IConvertible
        {
            var messagePipe = new MessagePipe<TMessageTypeEnum>(messengerType);
            registrar.Register<IMessagePipe<TMessageTypeEnum>>(() => messagePipe);

            if (initiatorAction != null)
            {
                initiatorAction.Invoke(messagePipe);
            }

            return messagePipe;
        }
    }
}
