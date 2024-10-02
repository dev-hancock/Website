// -----------------------------------------------------------------------
//  <copyright file="GitHubClient.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Services.GitHub
{
    using Models;

    public class GitHubClient
    {
        private readonly HttpClient _client;

        public GitHubClient(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("GitHub");
        }

        public Task<Resources> GetRateLimit(CancellationToken token = default)
        {
            return _client.GetFromJsonAsync<Resources>("rate_limit", token)!;
        }

        public Task<Repository[]> GetRepositories(string username, CancellationToken token = default)
        {
            return _client.GetFromJsonAsync<Repository[]>($"users/{username}/repos", token)!;
        }
    }
}