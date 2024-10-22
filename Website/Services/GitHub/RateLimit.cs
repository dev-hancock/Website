// -----------------------------------------------------------------------
//  <copyright file="RateLimit.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Services.GitHub
{
    using System.Text.Json.Serialization;

    public class RateLimit
    {
        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        [JsonPropertyName("remaining")]
        public int Remaining { get; set; }

        [JsonPropertyName("reset")]
        public int Reset { get; set; }

        [JsonPropertyName("used")]
        public int Used { get; set; }
    }
}