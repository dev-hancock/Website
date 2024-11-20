// -----------------------------------------------------------------------
//  <copyright file="ExternalDispatcher.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Server.Dispatchers
{
    using System.Text.Json;
    using Abstractions.Messages.Interfaces;
    using Abstractions.Pipeline;
    using Microsoft.AspNetCore.SignalR;

    public class ExternalDispatcher(IHubContext context) : IDispatcher
    {
        private readonly IHubContext _context = context;

        /// <inheritdoc />
        public bool CanHandle<T>(IMessage<T> message)
        {
            return message is IExternalEvent;
        }

        /// <inheritdoc />
        public async Task<T?> Dispatch<T>(IMessage<T> message, CancellationToken token)
        {
            await _context.Clients.All.SendAsync("Handle", JsonSerializer.Serialize(message, message.GetType()), token);

            return default;
        }
    }
}