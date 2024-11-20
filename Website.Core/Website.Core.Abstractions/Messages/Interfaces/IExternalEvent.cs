// -----------------------------------------------------------------------
//  <copyright file="IExternalEvent.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Abstractions.Messages.Interfaces
{
    public interface IExternalEvent : IEvent
    {
        string Name { get; }
    }
}