// -----------------------------------------------------------------------
//  <copyright file="ProjectMappings.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Application.Mappings
{
    using Domain.Models;
    using Shared.DTOs;

    public static class ProjectMappings
    {
        public static ProjectDto MapToDto(this Project project)
        {
            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Link = project.Link,
                Forks = project.Forks,
                Stars = project.Stars,
                Watchers = project.Watchers
            };
        }
    }
}