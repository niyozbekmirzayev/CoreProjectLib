using CoreProjectLib.Exceptions.ExceptionTypes;
using CoreProjectLib.Responses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CoreProjectLib.Exceptions
{
    public static class ExceptionHandlers
    {
        private const string RESPONSE_CONTENT_TYPE = "application/json";

        public static Task HandleBusinessLogicExceptionAsync(BusinessLogicException ex, HttpContext httpContext)
        {
            var response = new BaseResponse<object>()
            {
                IsSuccess = false,
                ErrorMessage = ex.Message
            };

            httpContext.Response.StatusCode = ex.StatusCode;
            httpContext.Response.ContentType = RESPONSE_CONTENT_TYPE;

            string responseJson = JsonConvert.SerializeObject(response);

            return httpContext.Response.WriteAsync(responseJson);
        }

        public static Task HandleExceptionAsync(Exception ex, HttpContext httpContext)
        {
            var response = new BaseResponse<object>()
            {
                IsSuccess = false,
                ErrorMessage = ex.Message
            };

            if (ex.InnerException != null)
            {
                response.ErrorMessage = ex.InnerException.Message;
            }

            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = RESPONSE_CONTENT_TYPE;

            string responseJson = JsonConvert.SerializeObject(response);

            return httpContext.Response.WriteAsync(responseJson);
        }
    }
}
