using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// One-of implementation.
namespace BuberDinner.Application.Common.Errors
{
    public class DuplicateEmailError : IError
    {
       public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "Email already exists.";
    }
}