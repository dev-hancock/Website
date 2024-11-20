// -----------------------------------------------------------------------
//  <copyright file="Mediator.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Messaging
{
    using Abstractions.Messages.Interfaces;
    using Abstractions.Pipeline;
    using Dispatchers;

    public sealed class Mediator(IEnumerable<IBehaviour> middleware, IEnumerable<IDispatcher> dispatchers) : IMediator
    {
        private readonly IDispatcher _dispatcher = new CompositeDispatcher(dispatchers);

        private readonly IBehaviour _pipeline = new Pipeline(middleware);

        /// <inheritdoc />
        public bool CanHandle<T>(IMessage<T> message)
        {
            return _dispatcher.CanHandle(message);
        }

        public Task<T?> Dispatch<T>(IMessage<T> message, CancellationToken token = default)
        {
            if (!CanHandle(message))
            {
                throw new ArgumentException($"Unexpected message type: {message.GetType()}");
            }

            return _pipeline.Invoke(message, _dispatcher, token);
        }
    }
}