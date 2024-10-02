// -----------------------------------------------------------------------
//  <copyright file="MailGunOptions.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Services.MailGun
{
    public class MailGunOptions
    {
        public string ApiKey { get; set; }

        public string Domain { get; set; }

        public string To { get; set; }
    }
}