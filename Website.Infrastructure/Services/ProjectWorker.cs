// -----------------------------------------------------------------------
//  <copyright file="ProjectWorker.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Infrastructure.Services
{
    using Application.Interfaces;
    using Core.Abstractions.Events;
    using Domain.Models;
    using Microsoft.Extensions.Hosting;

    public class ProjectWorker : BackgroundService
    {
        private static readonly TimeSpan DefaultDelay = TimeSpan.FromMinutes(1);

        private readonly Projects _projects;

        private readonly IEventPublisher _publisher;

        private readonly IProjectService _service;

        public ProjectWorker(IProjectService service, Projects projects, IEventPublisher publisher)
        {
            _service = service;
            _projects = projects;
            _publisher = publisher;
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken token)
        {
            do
            {
                try
                {
                    var projects = await _service.GetProjects("ahancock1", token);

                    _projects.Sync(projects);

                    _publisher.Raise(_projects);

                    await Task.Delay(DefaultDelay, token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception)
                {
                    await Task.Delay(DefaultDelay, token);
                }
            }
            while (!token.IsCancellationRequested);
        }
    }
}