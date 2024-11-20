// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Server
{
    using System.Threading.RateLimiting;
    using Application.Commands;
    using Application.Handlers;
    using Application.Interfaces;
    using Application.Queries;
    using Core.Abstractions.Handlers;
    using Core.Abstractions.Pipeline;
    using Core.Server;
    using Core.Server.Extensions;
    using Domain.Events;
    using Domain.Models;
    using FluentValidation;
    using Infrastructure.Behaviours;
    using Infrastructure.GitHub;
    using Infrastructure.RateLimiting;
    using Infrastructure.Services;
    using Microsoft.AspNetCore.SignalR;
    using Shared.DTOs;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCore();

            ConfigureServices(builder.Services);

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddSignalR();
            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseExceptionHandler("/error");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseWebAssemblyDebugging();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.MapHub<EventHub>("/events");

            app.Run();
        }

        private static void AddApplication(IServiceCollection services)
        {
            services.AddTransient<IHandler<GetProjectsQuery, ProjectDto[]>, GetProjectsQueryHandler>();
            services.AddTransient<IHandler<GetProjectQuery, ProjectDto>, GetProjectQueryHandler>();

            services.AddTransient<IHandler<ProjectCreatedDomainEvent>, ProjectEventHandlers>();
            services.AddTransient<IHandler<ProjectUpdatedDomainEvent>, ProjectEventHandlers>();
            services.AddTransient<IHandler<ProjectDeletedDomainEvent>, ProjectEventHandlers>();

            services.AddTransient<IValidator<ContactCommand>, ContactCommandValidator>();
        }

        private static void AddDomain(IServiceCollection services)
        {
            services.AddSingleton<Projects>();
        }

        private static void AddInfrastructure(IServiceCollection services)
        {
            services.AddTransient<IBehaviour, LoggingBehaviour>();
            services.AddTransient<IBehaviour, ValidationBehaviour>();

            services.AddHostedService<ProjectWorker>();
            services.AddTransient<IProjectService, GitHubService>();

            services.AddHttpClient<GitHubClient>(client =>
                {
                    client.BaseAddress = new Uri("https://api.github.com/");
                    client.DefaultRequestHeaders.Add("User-Agent", "Hancock-Software-Solutions-Limited");
                })
                .AddHttpMessageHandler<RateLimitingHandler>();
            services.AddSingleton<RateLimiter>(_ => new FixedWindowRateLimiter(
                new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 1,
                    Window = TimeSpan.FromSeconds(1),
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 0,
                    AutoReplenishment = true
                }));
            services.AddTransient<RateLimitingHandler>();

            services.AddSingleton<IHubContext>(sp => (IHubContext)sp.GetService<IHubContext<EventHub>>()!);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            AddApplication(services);
            AddDomain(services);
            AddInfrastructure(services);
        }
    }
}