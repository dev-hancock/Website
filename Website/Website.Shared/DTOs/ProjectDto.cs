// -----------------------------------------------------------------------
//  <copyright file="ProjectDto.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Shared.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public int Forks { get; set; }

        public int Stars { get; set; }

        public int Watchers { get; set; }
    }
}