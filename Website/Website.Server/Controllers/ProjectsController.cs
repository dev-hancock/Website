// -----------------------------------------------------------------------
//  <copyright file="ProjectsController.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Server.Controllers
{
    using Application.Queries;
    using Core.Abstractions.Pipeline;
    using Microsoft.AspNetCore.Mvc;
    using Shared.DTOs;

    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProject(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var project = await _mediator.Dispatch(
                new GetProjectQuery
                {
                    Id = id
                },
                HttpContext.RequestAborted);

            return Ok(project);
        }

        [HttpGet]
        public async Task<ActionResult<ProjectDto[]>> GetProjects()
        {
            return Ok(await _mediator.Dispatch(new GetProjectsQuery(), HttpContext.RequestAborted));
        }
    }
}