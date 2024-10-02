// -----------------------------------------------------------------------
//  <copyright file="MailGunService.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Services.MailGun
{
    using Microsoft.Extensions.Options;

    public class MailGunService
    {
        private readonly HttpClient _client;

        private readonly MailGunOptions _options;

        public MailGunService(HttpClient client, IOptions<MailGunOptions> options)
        {
            _client = client;
            _options = options.Value;
        }

        public async Task<bool> SendAsync(Message message)
        {
            var content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    {"to", _options.To},
                    {"from", $"no-reply@{_options.Domain}"},
                    {"subject", $"{message.Name}: {message.Subject}"},
                    {"text", $"{message.Body}"},
                    {"html", $"{message.Body}"},

                    //{"h:sender", message.Email},
                    {"h:reply-to", message.Email}
                });

            try
            {
                var response = await _client.PostAsync("messages", content);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();

                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}