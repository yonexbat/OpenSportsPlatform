using Microsoft.AspNetCore.Diagnostics;
using OpenSportsPlatform.Lib.Model.Exceptions;
using System.Net;
using System.Security;

namespace OpenSportsPlatform.Application.ErrorHandling
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
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
                        await HandleException(contextFeature.Error, context);
                    }                        
                });
            });
        }

        private static async Task HandleException(Exception exception, HttpContext context)
        {
            var errorDetails = new ErrorDetails();
            switch(exception)
            {
                case EntityNotFoundException:
                    errorDetails.StatusCode = StatusCodes.Status404NotFound;
                    errorDetails.Message = "Entity not found";
                    break;
                case SecurityException:
                    errorDetails.StatusCode = StatusCodes.Status403Forbidden;
                    errorDetails.Message = "Forbidden";
                    break;
                default:
                    errorDetails.StatusCode = context.Response.StatusCode;
                    errorDetails.Message = "An error occured";
                    break;
            }

            string message = errorDetails.ToString();

            context.Response.StatusCode = errorDetails.StatusCode;
            await context.Response.WriteAsync(message);
        }

    }
      
}
