using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using FluentResults;
using OneOf;

namespace BuberDinner.Application.Services.Authentication.Queries
{
    // CQRS - login is query as it does not changes/mutates the structure of data (DB).
    public interface IAuthenticationQueryService
    {
        ErrorOr<AuthenticationResult> Login(string email, string password);
       
    }
}