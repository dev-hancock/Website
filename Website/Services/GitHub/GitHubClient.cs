// -----------------------------------------------------------------------
//  <copyright file="GitHubClient.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Services.GitHub
{
    using System.Net.Http.Headers;
    using System.Text.Json;

    public class GitHubClient
    {
        private readonly HttpClient _client;

        public GitHubClient(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("GitHub");
        }

        public RateLimit? Limits { get; private set; }

        public Task<Repository[]?> GetRepositories(string username, CancellationToken token = default)
        {
            return Get<Repository[]>($"users/{username}/repos", token);
        }

        private async Task<T?> Get<T>(string url, CancellationToken token = default) where T : class
        {
            var response = await _client.GetAsync(url, token);

            UpdateRateLimits(response.Headers);

            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync(token);

                return await JsonSerializer.DeserializeAsync<T>(stream, JsonSerializerOptions.Default, token);
            }

            return default(T);
        }

        private T? GetHeaderValue<T>(HttpHeaders headers, string header)
        {
            if (headers.TryGetValues(header, out var values))
            {
                var value = values.FirstOrDefault();

                if (value != null)
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }

            return default(T);
        }

        private void UpdateRateLimits(HttpHeaders headers)
        {
            Limits = new RateLimit
            {
                Limit = GetHeaderValue<int>(headers, "x-ratelimit-limit"),
                Remaining = GetHeaderValue<int>(headers, "x-ratelimit-remaining"),
                Used = GetHeaderValue<int>(headers, "x-ratelimit-used"),
                Reset = GetHeaderValue<int>(headers, "x-ratelimit-reset")
            };
        }
    }
}