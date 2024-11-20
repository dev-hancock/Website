// -----------------------------------------------------------------------
//  <copyright file="ProjectEventHandlers.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Application.Handlers
{
    using System.Reactive;
    using Core.Abstractions.Handlers;
    using Core.Abstractions.Pipeline;
    using Domain.Events;
    using Mappings;
    using Shared.Events;

    public class ProjectEventHandlers(IMediator mediator) :
        IHandler<ProjectCreatedDomainEvent>,
        IHandler<ProjectUpdatedDomainEvent>,
        IHandler<ProjectDeletedDomainEvent>
    {
        private readonly IMediator _mediator = mediator;

        /// <inheritdoc />
        public Task<Unit> Handle(ProjectCreatedDomainEvent e, CancellationToken token = default)
        {
            return _mediator.Dispatch(
                new ProjectCreatedEvent
                {
                    Project = e.Project.MapToDto()
                },
                token);
        }

        /// <inheritdoc />
        public Task<Unit> Handle(ProjectUpdatedDomainEvent e, CancellationToken token = default)
        {
            return _mediator.Dispatch(
                new ProjectUpdatedEvent
                {
                    Project = e.Project.MapToDto()
                },
                token);
        }

        /// <inheritdoc />
        public Task<Unit> Handle(ProjectDeletedDomainEvent e, CancellationToken token = default)
        {
            return _mediator.Dispatch(
                new ProjectDeletedEvent
                {
                    Id = e.Id
                },
                token);
        }
    }
}