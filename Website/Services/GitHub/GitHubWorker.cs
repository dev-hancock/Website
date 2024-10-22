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
        private static readonly TimeSpan DefaultDelay = TimeSpan.FromMinutes(1);

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
            if (!IsValid(_options))
            {
                return;
            }

            do
            {
                try
                {
                    var repos = await _client.GetRepositories(_options!.Username!, token);

                    if (repos != null)
                    {
                        _state.Repos = repos.Where(x => !x.IsForked).ToArray();
                    }

                    await Task.Delay(GetRateLimitDelay(), token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception)
                {
                    await Task.Delay(DefaultDelay, token);
                }
            }
            while (!token.IsCancellationRequested);
        }

        private TimeSpan GetRateLimitDelay()
        {
            var limits = _client.Limits;

            if (limits is not {Reset: > 0, Remaining: > 0})
            {
                return DefaultDelay;
            }

            var reset = DateTimeOffset.FromUnixTimeSeconds(limits.Reset);

            var remaining = Math.Max(limits.Remaining, 1);

            var delay = ((reset - DateTime.UtcNow) / remaining).TotalSeconds;

            return TimeSpan.FromSeconds(Math.Max(delay, DefaultDelay.TotalSeconds));
        }

        private static bool IsValid(GithubOptions options)
        {
            return options is
            {
                Token: not null,
                Username: not null,
                BaseUrl: not null
            };
        }
    }
}