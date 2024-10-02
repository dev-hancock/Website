// -----------------------------------------------------------------------
//  <copyright file="GitHubWorker.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Services.GitHub
{
    using Microsoft.Extensions.Options;
    using State;

    public class GitHubWorker : BackgroundService
    {
        private readonly GitHubClient _client;

        private readonly GithubOptions _options;

        private readonly AppState _state;

        public GitHubWorker(AppState state, GitHubClient client, IOptions<GithubOptions> options)
        {
            _state = state;
            _client = client;
            _options = options.Value;
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var limits = await _client.GetRateLimit(token);

                    var reset = DateTimeOffset.FromUnixTimeSeconds(limits.Rate.Reset);

                    var remaining = Math.Max(limits.Rate.Remaining, 1);

                    var delay = (reset - DateTime.UtcNow) / remaining;

                    do
                    {
                        var repos = await _client.GetRepositories(_options.Username, token);

                        repos = repos.Where(x => !x.IsForked).ToArray();

                        _state.Repos = repos;

                        await Task.Delay((int)Math.Max(delay.TotalMilliseconds, 60e3), token);
                    }
                    while (DateTime.UtcNow < reset && !token.IsCancellationRequested);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception)
                {
                    await Task.Delay(TimeSpan.FromMinutes(1), token);
                }
            }
        }
    }
}