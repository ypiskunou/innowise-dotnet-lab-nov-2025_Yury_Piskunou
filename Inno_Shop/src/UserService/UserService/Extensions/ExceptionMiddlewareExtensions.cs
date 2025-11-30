using System.Net;
using System.Text.Json;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using UserService.Contracts;
using UserService.Entities.ErrorModel;
using UserService.Entities.Exceptions;

namespace UserService.Extensions;

public static class ExceptionMiddlewareExtensions 
{ 
    public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger) 
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
                    logger.LogError($"Something went wrong: {contextFeature.Error}");
                    
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        ValidationAppException => StatusCodes.Status422UnprocessableEntity,
                        NotFoundException => StatusCodes.Status404NotFound,
                        ForbiddenException => StatusCodes.Status403Forbidden,
                        BadRequestException => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    if (contextFeature.Error is ValidationAppException validationException)
                    {
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new 
                        { 
                            StatusCode = context.Response.StatusCode,
                            Message = "Validation Failed",
                            Errors = validationException.Errors 
                        }));
                    }
                    else
                    {
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());
                    }
                }
            });
        }); 
    } 
} 