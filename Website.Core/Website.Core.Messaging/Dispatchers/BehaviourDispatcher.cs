// -----------------------------------------------------------------------
//  <copyright file="BehaviourDispatcher.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Messaging.Dispatchers
{
    using Abstractions.Messages.Interfaces;
    using Abstractions.Pipeline;

    public class BehaviourDispatcher(IBehaviour behaviour, IDispatcher inner) : IDispatcher
    {
        private readonly IBehaviour _behaviour = behaviour;

        private readonly IDispatcher _inner = inner;

        /// <inheritdoc />
        public bool CanHandle<T>(IMessage<T> message)
        {
            return _inner.CanHandle(message);
        }

        /// <inheritdoc />
        public Task<T?> Dispatch<T>(IMessage<T> message, CancellationToken token = default)
        {
            return _behaviour.Invoke(message, _inner, token);
        }
    }
}