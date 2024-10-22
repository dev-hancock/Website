// -----------------------------------------------------------------------
//  <copyright file="Message.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Services.MailGun
{
    public class Message
    {
        public string Body { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;
    }
}