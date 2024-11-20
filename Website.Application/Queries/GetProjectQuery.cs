// -----------------------------------------------------------------------
//  <copyright file="GetProjectQuery.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Application.Queries
{
    using System.Reactive.Linq;
    using Core.Abstractions.Handlers;
    using Core.Abstractions.Messages.Interfaces;
    using Domain.Models;
    using Mappings;
    using Shared.DTOs;

    public class GetProjectQuery : IQuery<ProjectDto>
    {
        public int Id { get; set; }
    }

    public class GetProjectQueryHandler(Projects projects) : IHandler<GetProjectQuery, ProjectDto>
    {
        private readonly Projects _projects = projects;

        /// <inheritdoc />
        public async Task<ProjectDto> Handle(GetProjectQuery e, CancellationToken token = default)
        {
            try
            {
                var project = await _projects.Items
                    .ToObservable()
                    .Where(x => x.Id == e.Id)
                    .Select(x => x.MapToDto())
                    .SingleAsync();

                return project;
            }
            catch (InvalidOperationException ex)
            {
                throw new KeyNotFoundException($"Project with Id {e.Id} not found.", ex);
            }
        }
    }
}