// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website
{
    using System.Reflection;
    using System.Text;
    using Components;
    using Microsoft.Extensions.Options;
    using Serilog;
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
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.secrets.json", optional: true)
                .AddEnvironmentVariables();

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
            var config = services.GetService<Config>()!;

            var options = config.Github!;

            client.BaseAddress = new Uri($"{options.BaseUrl}");
            client.DefaultRequestHeaders.Add("User-Agent", GetUserAgent(config));
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.Token}");
        }

        private static string GetUserAgent(Config config)
        {
            return $"{config.Name}/{Assembly.GetExecutingAssembly().GetName().Version} ({config.Website})";
        }

        private static void ConfigureMailGun(IServiceProvider services, HttpClient client)
        {
            var config = services.GetService<Config>()!;

            var options = config.MailGun!;

            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"api:{options.ApiKey}"));

            client.BaseAddress = new Uri($"{options.BaseUrl}v3/{options.Domain}/");
            client.DefaultRequestHeaders.Add("User-Agent", GetUserAgent(config));
            client.DefaultRequestHeaders.Add("Authorization", $"Basic api:{token}");
        }

        private static void ConfigureOptions<T>(IServiceCollection services, IConfigurationSection section) where T : class
        {
            services.Configure<T>(section);
            services.AddTransient(cfg => cfg.GetService<IOptions<T>>()!.Value);
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services.AddSerilog();

            services.AddSingleton(new AppState());

            ConfigureOptions<Config>(services, config.GetSection("Config"));
            
            ConfigureOptions<CompanyOptions>(services, config.GetSection("Config:Company"));

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