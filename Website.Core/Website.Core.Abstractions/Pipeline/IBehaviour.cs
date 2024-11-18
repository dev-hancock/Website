// -----------------------------------------------------------------------
//  <copyright file="IBehaviour.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Abstractions.Pipeline
{
    using Messages.Interfaces;

    public interface IBehaviour
    {
        Task<T?> Invoke<T>(IMessage<T> message, IDispatcher inner, CancellationToken token = default);
    }
}