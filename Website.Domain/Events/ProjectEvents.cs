// -----------------------------------------------------------------------
//  <copyright file="ProjectEvents.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Domain.Events
{
    using Core.Abstractions.Messages;
    using Models;

    public class ProjectDeletedDomainEvent : InternalEvent
    {
        public int Id { get; set; }
    }

    public class ProjectCreatedDomainEvent : InternalEvent
    {
        public Project Project { get; set; }
    }

    public class ProjectUpdatedDomainEvent : InternalEvent
    {
        public Project Project { get; set; }
    }
}