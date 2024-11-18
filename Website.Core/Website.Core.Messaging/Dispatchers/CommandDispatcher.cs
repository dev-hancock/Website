// -----------------------------------------------------------------------
//  <copyright file="CommandDispatcher.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Messaging.Dispatchers
{
    using Abstractions.Messages.Interfaces;

    public class CommandDispatcher(IServiceProvider services) : MessageDispatcher(services)
    {
        /// <inheritdoc />
        public override bool CanHandle<T>(IMessage<T> message)
        {
            return message is ICommand;
        }

        /// <inheritdoc />
        public override async Task<T?> Dispatch<T>(IMessage<T> message, CancellationToken token = default) where T : default
        {
            foreach (var handler in GetHandlers(message.GetType()))
            {
                await Invoke(handler, message, token);
            }

            return default;
        }
    }
}