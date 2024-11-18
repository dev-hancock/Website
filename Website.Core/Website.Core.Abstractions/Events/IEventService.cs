// -----------------------------------------------------------------------
//  <copyright file="IEventService.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Abstractions.Events
{
    using Messages.Interfaces;

    public interface IEventService : IAsyncDisposable
    {
        IObservable<T> OfType<T>() where T : IExternalEvent;

        Task StartAsync();
    }
}