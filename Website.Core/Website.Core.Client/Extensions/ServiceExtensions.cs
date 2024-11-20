// -----------------------------------------------------------------------
//  <copyright file="ServiceExtensions.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Client.Extensions
{
    using Abstractions.Events;
    using Events;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, Action<EventOptions>? configure)
        {
            var options = new EventOptions();

            configure?.Invoke(options);

            services.AddSingleton(_ => new EventFactory(options.Assembly));
            services.AddSingleton<IEventService, EventService>(x => ActivatorUtilities.CreateInstance<EventService>(x, options.Hub));

            return services;
        }
    }
}