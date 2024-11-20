// -----------------------------------------------------------------------
//  <copyright file="CompositeDispatcher.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Messaging.Dispatchers
{
    using Abstractions.Messages.Interfaces;
    using Abstractions.Pipeline;

    public class CompositeDispatcher(IEnumerable<IDispatcher> dispatchers) : IDispatcher
    {
        private readonly IEnumerable<IDispatcher> _dispatchers = dispatchers;

        /// <inheritdoc />
        public bool CanHandle<T>(IMessage<T> message)
        {
            return _dispatchers.Any(x => x.CanHandle(message));
        }

        /// <inheritdoc />
        public async Task<T?> Dispatch<T>(IMessage<T> message, CancellationToken token = default)
        {
            foreach (var dispatcher in _dispatchers)
            {
                if (dispatcher.CanHandle(message))
                {
                    return await dispatcher.Dispatch(message, token);
                }
            }

            return default;
        }
    }
}