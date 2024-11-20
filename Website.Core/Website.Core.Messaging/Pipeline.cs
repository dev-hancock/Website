// -----------------------------------------------------------------------
//  <copyright file="Pipeline.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Messaging
{
    using Abstractions.Messages.Interfaces;
    using Abstractions.Pipeline;
    using Dispatchers;

    public class Pipeline(IEnumerable<IBehaviour> middleware) : IPipeline
    {
        private readonly IEnumerable<IBehaviour> _middleware = middleware;

        public Task<T?> Invoke<T>(IMessage<T> message, IDispatcher inner, CancellationToken token = default)
        {
            var current = inner;

            foreach (var middleware in _middleware)
            {
                current = new BehaviourDispatcher(middleware, current);
            }

            return current.Dispatch(message, token);
        }
    }
}