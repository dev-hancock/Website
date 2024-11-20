// -----------------------------------------------------------------------
//  <copyright file="IEvent.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Abstractions.Messages.Interfaces
{
    using System.Reactive;

    public interface IEvent : IMessage<Unit>;
}