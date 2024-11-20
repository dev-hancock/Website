// -----------------------------------------------------------------------
//  <copyright file="LoggingBehaviour.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Infrastructure.Behaviours
{
    using Core.Abstractions.Messages.Interfaces;
    using Core.Abstractions.Pipeline;

    public class LoggingBehaviour : IBehaviour
    {
        /// <inheritdoc />
        public async Task<T?> Invoke<T>(IMessage<T> message, IDispatcher inner, CancellationToken token = default)
        {
            try
            {
                var result = await inner.Dispatch(message, token);

                Console.WriteLine($"Dispatched: {message.GetType().Name}");

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                return default;
            }
        }
    }
}