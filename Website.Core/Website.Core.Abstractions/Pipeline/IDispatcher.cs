// -----------------------------------------------------------------------
//  <copyright file="IDispatcher.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Abstractions.Pipeline
{
    using Messages.Interfaces;

    public interface IDispatcher
    {
        bool CanHandle<T>(IMessage<T> message);

        Task<T?> Dispatch<T>(IMessage<T> message, CancellationToken token = default);
    }
}