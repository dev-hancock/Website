// -----------------------------------------------------------------------
//  <copyright file="EventExtensions.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Client.Extensions
{
    using System.Reflection;
    using Abstractions.Messages.Interfaces;

    public static class EventExtensions
    {
        public static Dictionary<string, Type> GetEvents(this Assembly assembly)
        {
            var types = new Dictionary<string, Type>();

            foreach (var type in assembly.GetTypes().Where(IsEvent))
            {
                try
                {
                    var instance = (IExternalEvent)Activator.CreateInstance(type)!;

                    types[instance.Name] = type;
                }
                catch
                {
                    // ignored
                }
            }

            return types;
        }

        private static bool IsEvent(Type type)
        {
            return typeof(IExternalEvent).IsAssignableFrom(type) && type is
            {
                IsInterface: false,
                IsAbstract: false
            };
        }
    }
}