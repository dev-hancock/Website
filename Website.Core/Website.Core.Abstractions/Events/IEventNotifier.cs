// -----------------------------------------------------------------------
//  <copyright file="IEventNotifier.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Abstractions.Events
{
    using Models;

    public interface IEventPublisher
    {
        void Raise(Aggregate aggregate);
    }
}