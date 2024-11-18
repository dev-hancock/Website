// -----------------------------------------------------------------------
//  <copyright file="Project.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Domain.Models
{
    using Core.Abstractions.Models;

    // TODO: Make immutable
    public class Project : Entity<int>
    {
        public string Description { get; set; } = string.Empty;

        public int Forks { get; set; } = 0;

        public bool IsForked { get; set; } = false;

        public string Link { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int Stars { get; set; } = 0;

        public int Watchers { get; set; } = 0;

        public override bool Equals(object? obj)
        {
            if (obj is not Project other)
            {
                return false;
            }

            return Name == other.Name &&
                Description == other.Description &&
                Forks == other.Forks &&
                IsForked == other.IsForked &&
                Link == other.Link &&
                Stars == other.Stars &&
                Watchers == other.Watchers;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Forks, IsForked, Link, Stars, Watchers);
        }
    }
}