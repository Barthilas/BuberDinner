using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuberDinner.Api.Middleware
{
    public class ErrorHandlingMidleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMidleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try{
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
           var code = HttpStatusCode.InternalServerError;
           var result = JsonSerializer.Serialize(new {error = "An error occured while processing your request."});
           context.Response.ContentType = "application/json";
           context.Response.StatusCode = (int)code;
           return context.Response.WriteAsync(result);
        }
    }
}