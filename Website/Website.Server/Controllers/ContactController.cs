// -----------------------------------------------------------------------
//  <copyright file="ContactController.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Server.Controllers
{
    using Application.Commands;
    using Core.Abstractions.Pipeline;
    using Microsoft.AspNetCore.Mvc;
    using Shared.DTOs;

    [ApiController]
    [Route("api/[controller]")]
    public class ContactController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult> Contact([FromBody] MessageDto? message)
        {
            if (message == null)
            {
                return BadRequest("Message cannot be null");
            }

            await _mediator.Dispatch(
                new ContactCommand
                {
                    Name = "",
                    Email = message.Email,
                    Subject = message.Subject,
                    Body = message.Body
                }, 
                HttpContext.RequestAborted);

            return Ok();
        }
    }
}