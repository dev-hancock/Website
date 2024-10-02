// -----------------------------------------------------------------------
//  <copyright file="Resources.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Services.GitHub
{
    using System.Text.Json.Serialization;

    public class Resources
    {
        [JsonPropertyName("rate")]
        public RateLimit Rate { get; set; }
    }
}