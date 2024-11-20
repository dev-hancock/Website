// -----------------------------------------------------------------------
//  <copyright file="ServiceExtensions.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Server.Extensions
{
    using Abstractions.Events;
    using Abstractions.Pipeline;
    using Dispatchers;
    using Domain.Events;
    using Messaging;
    using Messaging.Dispatchers;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IMediator, Mediator>();

            // CQRS
            services.AddTransient<IDispatcher, CommandDispatcher>();
            services.AddTransient<IDispatcher, QueryDispatcher>();

            // Events
            services.AddTransient<IDispatcher, InternalDispatcher>();
            services.AddTransient<IDispatcher, ExternalDispatcher>();

            services.AddTransient<IEventPublisher, EventPublisher>();

            return services;
        }
    }
}