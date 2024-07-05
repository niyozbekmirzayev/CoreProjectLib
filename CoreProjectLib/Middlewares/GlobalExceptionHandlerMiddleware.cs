using CoreProjectLib.Exceptions;
using CoreProjectLib.Exceptions.ExceptionTypes;
using Microsoft.AspNetCore.Http;

namespace CoreProjectLib.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (BusinessLogicException ex)
            {
                await ExceptionHandlers.HandleBusinessLogicExceptionAsync(ex, httpContext);
            }
            catch (Exception ex)
            {
                await ExceptionHandlers.HandleExceptionAsync(ex, httpContext);
            }
        }
    }
}
