// -----------------------------------------------------------------------
//  <copyright file="Repository.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Infrastructure.GitHub.Models
{
    using System.Text.Json.Serialization;

    public class Repository
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("forks_count")]
        public int Forks { get; set; } = 0;

        [JsonPropertyName("id")]
        public int Id { get; set; } = -1;

        [JsonPropertyName("fork")]
        public bool IsForked { get; set; } = false;

        [JsonPropertyName("html_url")]
        public string Link { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("stargazers_count")]
        public int Stars { get; set; } = 0;

        [JsonPropertyName("watchers_count")]
        public int Watchers { get; set; } = 0;
    }
}