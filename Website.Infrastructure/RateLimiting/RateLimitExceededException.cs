// -----------------------------------------------------------------------
//  <copyright file="RateLimitExceededException.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Infrastructure.RateLimiting
{
    public class RateLimitExceededException(string message) : Exception(message);
}