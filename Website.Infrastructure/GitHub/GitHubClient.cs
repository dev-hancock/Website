// -----------------------------------------------------------------------
//  <copyright file="GitHubClient.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Infrastructure.GitHub
{
    using System.Net.Http.Json;
    using Models;

    public class GitHubClient(HttpClient client)
    {
        private readonly HttpClient _client = client;

        public Task<Repository[]?> GetRepositories(string username, CancellationToken token = default)
        {
            return _client.GetFromJsonAsync<Repository[]>($"users/{username}/repos", token);
        }
    }
}