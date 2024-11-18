// -----------------------------------------------------------------------
//  <copyright file="ExceptionMiddleware.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Server.Middleware
{
    using Microsoft.AspNetCore.Diagnostics;
    using System.Net.WebSockets;

    public class ExceptionMiddleware(IEnumerable<IExceptionHandler> handlers, RequestDelegate next)
    {
        private readonly IEnumerable<IExceptionHandler> _handlers = handlers;

        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var token = context.RequestAborted;

                foreach(var handler in _handlers)
                {
                    if (await handler.TryHandleAsync(context, ex, token))
                    {
                        return;
                    }
                }

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var details = new
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = ex.Message,
                    Instance = context.Request.Path
                };

                await context.Response.WriteAsJsonAsync(details, token);
            }
        }
    }
}