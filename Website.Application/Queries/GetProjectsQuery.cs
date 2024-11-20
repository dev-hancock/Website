// -----------------------------------------------------------------------
//  <copyright file="GetProjectsQuery.cs" company="Hancock Software Solutions Limited">
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

    public class GetProjectsQuery : IQuery<ProjectDto[]>;

    public class GetProjectsQueryHandler(Projects projects) : IHandler<GetProjectsQuery, ProjectDto[]>
    {
        private readonly Projects _projects = projects;

        /// <inheritdoc />
        public async Task<ProjectDto[]> Handle(GetProjectsQuery e, CancellationToken token = default)
        {
            var projects = await _projects.Items
                .ToObservable()
                .Select(x => x.MapToDto())
                .ToArray();

            return projects ?? Array.Empty<ProjectDto>();
        }
    }
}