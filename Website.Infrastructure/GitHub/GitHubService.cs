// -----------------------------------------------------------------------
//  <copyright file="GitHubService.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Infrastructure.GitHub
{
    using Application.Interfaces;
    using Domain.Models;

    public class GitHubService(GitHubClient client) : IProjectService
    {
        private readonly GitHubClient _client = client;

        /// <inheritdoc />
        public async Task<Project[]> GetProjects(string username, CancellationToken token = default)
        {
            var repos = await _client.GetRepositories(username, token);

            var projects = repos?
                .Select(x => new Project
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Link = x.Link,
                    Forks = x.Forks,
                    Stars = x.Stars,
                    Watchers = x.Watchers,
                    IsForked = x.IsForked
                })
                .ToArray();

            return projects ?? Array.Empty<Project>();
        }
    }
}