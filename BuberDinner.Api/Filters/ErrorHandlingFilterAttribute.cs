using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuberDinner.Api.Filters
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var problemDetails = new ProblemDetails{
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "An error occured while processing your request.",
                Status = (int)HttpStatusCode.InternalServerError,
                //optional for the specification of problem json
                // Instance = context.HttpContext.Request.Path,
                // Detail = exception.Message
            };

            context.Result = new ObjectResult(problemDetails)
            {
                StatusCode = 500,
            };

            context.ExceptionHandled = true;
        }
    }
}