// -----------------------------------------------------------------------
//  <copyright file="IHandler.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Abstractions.Handlers
{
    using System.Reactive;
    using Messages.Interfaces;

    public interface IHandler<in TMessage, TResult> where TMessage : IMessage<TResult>
    {
        Task<TResult> Handle(TMessage message, CancellationToken token = default);
    }

    public interface IHandler<in TMessage> : IHandler<TMessage, Unit> where TMessage : IMessage<Unit>;
}