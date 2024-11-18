// -----------------------------------------------------------------------
//  <copyright file="EventOptions.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Client
{
    using System.Reflection;

    public class EventOptions
    {
        public Assembly Assembly { get; set; } = null!;

        public Uri Hub { get; set; } = null!;
    }
}