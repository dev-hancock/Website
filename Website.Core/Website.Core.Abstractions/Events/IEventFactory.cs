// -----------------------------------------------------------------------
//  <copyright file="IEventFactory.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Abstractions.Events
{
    using Messages.Interfaces;

    public interface IEventFactory
    {
        IExternalEvent? Create(string json);
    }
}