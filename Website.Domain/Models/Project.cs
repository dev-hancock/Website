// -----------------------------------------------------------------------
//  <copyright file="Project.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Domain.Models
{
    using Core.Abstractions.Models;

    public class Project : Entity<int>
    {
        public required string Description { get; init; }

        public required int Forks { get; init; }

        public required bool IsForked { get; init; }

        public required string Link { get; init; }

        public required string Name { get; init; }

        public required int Stars { get; init; }

        public required int Watchers { get; init; }

        public override bool Equals(object? obj)
        {
            if (obj is not Project other)
            {
                return false;
            }

            return Id == other.Id &&
                Name == other.Name &&
                Description == other.Description &&
                Forks == other.Forks &&
                IsForked == other.IsForked &&
                Link == other.Link &&
                Stars == other.Stars &&
                Watchers == other.Watchers;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Description, Forks, IsForked, Link, Stars, Watchers);
        }
    }
}