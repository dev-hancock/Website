// -----------------------------------------------------------------------
//  <copyright file="ValidationExceptionHandler.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Server.Middleware.Exceptions
{
    using FluentValidation;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    public class ValidationExceptionHandler : IExceptionHandler
    {
        /// <inheritdoc />
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken token)
        {
            if (exception is ValidationException ex)
            {
                var response = context.Response;

                var errors = ex.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Select(y => y.ErrorMessage).ToArray());

                var result = new BadRequestObjectResult(errors);

                await response.WriteAsJsonAsync(result, token);

                return true;
            }

            return false;
        }
    }
}