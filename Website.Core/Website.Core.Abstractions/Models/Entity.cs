// -----------------------------------------------------------------------
//  <copyright file="Entity.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Abstractions.Models
{
    public class Entity<T>
    {
        public T? Id { get; set; } = default(T);
    }
}