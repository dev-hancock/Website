// -----------------------------------------------------------------------
//  <copyright file="Aggregate.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Abstractions.Models
{
    using Messages.Interfaces;

    public class Aggregate
    {
        public List<IInternalEvent> Events { get; } = new();

        public object Lock { get; } = new();
    }
}