// -----------------------------------------------------------------------
//  <copyright file="Config.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Settings
{
    using Services.GitHub;
    using Services.MailGun;

    public class Config
    {
        public string[] Address { get; set; } =
        {
            "7b Mayors Buildings Rd",
            "Bristol",
            "BS16 5AU"
        };

        public string Name { get; set; } = default!;

        public string Website { get; set; } = default!;

        public CompanyOptions? Company { get; set; }

        public string? Email { get; set; }

        public GithubOptions? Github { get; set; }

        public MailGunOptions? MailGun { get; set; }

        public int? Year { get; set; } = 2024;
    }
}