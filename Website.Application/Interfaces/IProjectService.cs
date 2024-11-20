// -----------------------------------------------------------------------
//  <copyright file="IProjectService.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Application.Interfaces
{
    using Domain.Models;

    public interface IProjectService
    {
        Task<Project[]> GetProjects(string username, CancellationToken token = default);
    }
}