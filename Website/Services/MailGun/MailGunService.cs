// -----------------------------------------------------------------------
//  <copyright file="MailGunService.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Services.MailGun
{
    using Microsoft.Extensions.Options;
    using Serilog;

    public class MailGunService
    {
        private static readonly ILogger _logger = Log.ForContext<MailGunService>();

        private readonly HttpClient _client;

        private readonly MailGunOptions _options;

        public MailGunService(HttpClient client, IOptions<MailGunOptions> options)
        {
            _client = client;
            _options = options.Value;
        }

        public async Task<bool> SendAsync(Message message)
        {
            if (!IsValid(_options))
            {
                _logger.Error("Invalid messaging options");
                return false;
            }

            var content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    {"to", _options.To!},
                    {"from", $"no-reply@{_options.Domain!}"},
                    {"subject", $"{message.Name}: {message.Subject}"},
                    {"text", $"{message.Body}"},
                    {"html", $"{message.Body}"},
                    {"h:reply-to", message.Email}
                });

            try
            {
                var response = await _client.PostAsync("messages", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                var error = await response.Content.ReadAsStringAsync();

                _logger.Error($"Failed to send message: {error}");
                
                return false;
            }
            catch(HttpRequestException ex)
            {
                _logger.Error(ex, "Error sending message due to an HTTP request issue.");
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An unexpected error occured when sending a message.");
                return false;
            }
        }

        private static bool IsValid(MailGunOptions options)
        {
            return options is
            {
                ApiKey: not null,
                Domain: not null,
                To: not null,
                BaseUrl: not null
            };
        }
    }
}