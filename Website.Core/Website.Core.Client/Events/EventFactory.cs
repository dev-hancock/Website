// -----------------------------------------------------------------------
//  <copyright file="EventFactory.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Client.Events
{
    using System.Reflection;
    using System.Text.Json;
    using Abstractions.Events;
    using Abstractions.Messages.Interfaces;
    using Extensions;

    public class EventFactory(Assembly assembly) : IEventFactory
    {
        private readonly Dictionary<string, Type> _types = assembly.GetEvents();

        public IExternalEvent? Create(string json)
        {
            var document = JsonDocument.Parse(json);

            if (!document.RootElement.TryGetProperty(nameof(IExternalEvent.Name), out var element))
            {
                return default(IExternalEvent);
            }

            if (!_types.TryGetValue(element.GetString() ?? string.Empty, out var type))
            {
                return default(IExternalEvent);
            }

            return (IExternalEvent)document.Deserialize(type)!;
        }
    }
}