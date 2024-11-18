// -----------------------------------------------------------------------
//  <copyright file="ExternalEvent.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Abstractions.Messages
{
    using Interfaces;

    public abstract class ExternalEvent : IExternalEvent
    {
        public string Name => GetType().Name;
    }
}