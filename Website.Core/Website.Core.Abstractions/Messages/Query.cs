// -----------------------------------------------------------------------
//  <copyright file="Query.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Abstractions.Messages
{
    using Interfaces;

    public abstract class Query<T> : IQuery<T>;
}