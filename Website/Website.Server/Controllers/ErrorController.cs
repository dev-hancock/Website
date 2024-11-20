// -----------------------------------------------------------------------
//  <copyright file="ErrorController.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Server.Controllers
{
    using FluentValidation;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            switch (context?.Error)
            {
                case ValidationException ex:
                {
                    var errors = ex.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        );

                    return BadRequest(new
                    {
                        errors
                    });
                }
                case KeyNotFoundException ex:
                {
                    return NotFound(new {error = ex.Message});
                }
            }

            return StatusCode(500,
                new
                {
                    error = "An unexpected error occurred"
                });
        }
    }
}