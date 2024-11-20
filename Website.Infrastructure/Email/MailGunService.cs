// -----------------------------------------------------------------------
//  <copyright file="MailGunService.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Infrastructure.Email
{
    using Application.Interfaces;

    public class MailGunService : IEmailService
    {
        /// <inheritdoc />
        public Task SendAsync(string from, string to, string subject, string body, CancellationToken token = default)
        {
            throw new NotImplementedException("Email is not configured.");
        }
    }
}