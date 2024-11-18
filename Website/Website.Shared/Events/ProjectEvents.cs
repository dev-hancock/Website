// -----------------------------------------------------------------------
//  <copyright file="ProjectEvents.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Shared.Events
{
    using Core.Abstractions.Messages;
    using DTOs;

    public class ProjectCreatedEvent : ExternalEvent
    {
        public ProjectDto Project { get; set; } = default!;
    }

    public class ProjectDeletedEvent : ExternalEvent
    {
        public int Id { get; set; }
    }

    public class ProjectUpdatedEvent : ExternalEvent
    {
        public ProjectDto Project { get; set; } = default!;
    }
}