// -----------------------------------------------------------------------
//  <copyright file="Repository.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Models
{
    using System.Text.Json.Serialization;

    public class Repository
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("forks_count")]
        public int Forks { get; set; }

        [JsonPropertyName("fork")]
        public bool IsForked { get; set; }

        [JsonPropertyName("html_url")]
        public string Link { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("stargazers_count")]
        public int Stars { get; set; }

        [JsonPropertyName("watchers_count")]
        public int Watchers { get; set; }
    }
}