// -----------------------------------------------------------------------
//  <copyright file="Projects.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Domain.Models
{
    using Core.Abstractions.Models;
    using Events;

    public class Projects : Aggregate
    {
        private readonly Dictionary<int, Project> _items = new();

        public IEnumerable<Project> Items => _items.Values;

        public void Sync(IEnumerable<Project> projects)
        {
            foreach (var item in _items.Where(x => projects.All(y => y.Id != x.Key)))
            {
                Remove(item.Value);
            }

            foreach (var item in projects)
            {
                AddOrUpdate(item);
            }
        }

        private void Add(Project project)
        {
            if (_items.TryAdd(project.Id, project))
            {
                Events.Add(new ProjectCreatedDomainEvent
                {
                    Project = project
                });
            }
        }

        private void AddOrUpdate(Project project)
        {
            if (!_items.ContainsKey(project.Id))
            {
                Add(project);
            }
            else
            {
                Update(project);
            }
        }

        private bool HasChanged(Project project)
        {
            return _items.TryGetValue(project.Id, out var existing) && !existing.Equals(project);
        }

        private void Remove(Project project)
        {
            if (_items.Remove(project.Id))
            {
                Events.Add(new ProjectDeletedDomainEvent
                {
                    Id = project.Id
                });
            }
        }

        private void Update(Project project)
        {
            if (HasChanged(project))
            {
                _items[project.Id] = project;

                Events.Add(new ProjectUpdatedDomainEvent
                {
                    Project = project
                });
            }
        }
    }
}