// -----------------------------------------------------------------------
//  <copyright file="IEmailService.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string from, string to, string subject, string body, CancellationToken token = default);
    }
}