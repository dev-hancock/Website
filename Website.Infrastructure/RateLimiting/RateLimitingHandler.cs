// -----------------------------------------------------------------------
//  <copyright file="RateLimitingHandler.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Infrastructure.RateLimiting
{
    using System.Threading.RateLimiting;

    public class RateLimitingHandler(RateLimiter limiter) : DelegatingHandler
    {
        private readonly RateLimiter _limiter = limiter ?? throw new ArgumentNullException(nameof(limiter));

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            var lease = await _limiter.AcquireAsync(1, token);

            if (lease.IsAcquired)
            {
                try
                {
                    return await base.SendAsync(request, token);
                }
                finally
                {
                    lease.Dispose();
                }
            }

            throw new RateLimitExceededException("Rate limit exceeded. Please try again later.");
        }
    }
}