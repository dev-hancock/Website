// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website
{
    using System.Text;
    using Components;
    using Microsoft.Extensions.Options;
    using Services.GitHub;
    using Services.MailGun;
    using Settings;
    using State;

    public class Program
    {
        public static void Main(string[] args)
        {
            Build(args).Run();
        }

        private static WebApplication Build(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            Configure(app);

            return app;
        }

        private static void Configure(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
        }

        private static void ConfigureGithub(IServiceProvider services, HttpClient client)
        {
            var options = services.GetService<GithubOptions>()!;

            client.BaseAddress = new Uri("https://api.github.com/");
            client.DefaultRequestHeaders.Add("User-Agent", "Hancock.Software.Solutions");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.Token}");
        }

        private static void ConfigureMailGun(IServiceProvider services, HttpClient client)
        {
            var options = services.GetService<MailGunOptions>()!;

            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"api:{options.ApiKey}"));

            client.BaseAddress = new Uri($"https://api.mailgun.net/v3/{options.Domain}/");
            client.DefaultRequestHeaders.Add("User-Agent", "Hancock.Software.Solutions");
            client.DefaultRequestHeaders.Add("Authorization", $"Basic api:{token}");
        }

        private static void ConfigureOptions<T>(IServiceCollection services, IConfigurationSection section) where T : class
        {
            services.Configure<T>(section);
            services.AddTransient(cfg => cfg.GetService<IOptions<T>>()!.Value);
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(new AppState());

            ConfigureOptions<Config>(services, config.GetSection("Config"));

            ConfigureOptions<MailGunOptions>(services, config.GetSection("Config:MailGun"));

            ConfigureOptions<GithubOptions>(services, config.GetSection("Config:Github"));

            services.AddSingleton<GitHubClient>();

            services.AddHttpClient<GitHubWorker>("GitHub", ConfigureGithub);

            services.AddHttpClient<MailGunService>(ConfigureMailGun);

            services.AddHostedService<GitHubWorker>();

            services.AddRazorComponents().AddInteractiveServerComponents();
        }
    }
}