using GerenciadorCursos.API.Models;
using GerenciadorCursos.CrossCutting.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using CustomValidationException = GerenciadorCursos.CrossCutting.Exceptions.ValidationException;

namespace GerenciadorCursos.CrossCutting.Extensions
{
    public static class APIExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        int statusCode = context.Response.StatusCode;
                        string message = contextFeature.Error.Message;

                        switch (contextFeature.Error)
                        {
                            case BusinessException:
                                statusCode = (int)HttpStatusCode.BadRequest;
                                break;
                            case CustomValidationException:
                                statusCode = (int)HttpStatusCode.UnprocessableEntity;
                                break;
                        }

                        var error = new ErrorDetails
                        {
                            StatusCode = statusCode,
                            Message = message,
                            Trace = env.IsDevelopment() ? contextFeature.Error.StackTrace : null
                        };

                        context.Response.StatusCode = statusCode;
                        await context.Response.WriteAsync(JsonSerializer.Serialize(error));
                    }
                });
            });
        }
    }
}
