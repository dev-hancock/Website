// -----------------------------------------------------------------------
//  <copyright file="QueryDispatcher.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Messaging.Dispatchers
{
    using Abstractions.Messages.Interfaces;

    public class QueryDispatcher(IServiceProvider services) : MessageDispatcher(services)
    {
        /// <inheritdoc />
        public override bool CanHandle<T>(IMessage<T> message)
        {
            return message is IQuery<T>;
        }

        /// <inheritdoc />
        public override Task<T?> Dispatch<T>(IMessage<T> message, CancellationToken token = default) where T : default
        {
            return Invoke(GetHandler<T>(message.GetType()), message, token);
        }
    }
}