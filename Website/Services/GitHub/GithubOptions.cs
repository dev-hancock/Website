// -----------------------------------------------------------------------
//  <copyright file="GithubOptions.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Services.GitHub
{
    public class GithubOptions
    {
        public string BaseAddress { get; set; } = "https://api.github.com/";

        public string Token { get; set; }

        public string Username { get; set; }
    }
}