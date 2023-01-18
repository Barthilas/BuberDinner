using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Application.Common.Errors;
using ErrorOr;
using FluentResults;
using OneOf;

namespace BuberDinner.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        // AuthenticationResult Register(string firstName, string lastName, string email, string password);
        // bad scalability (tho)
        // OneOf<AuthenticationResult, DuplicateEmailError> Register(string firstName, string lastName, string email, string password);
        // IError can be List<> but that becomes mess quickly.
        // OneOf<AuthenticationResult, IError> Register(string firstName, string lastName, string email, string password);
        

        // FluentResult to the resque.
        // Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password);

        // ErrorOr
        ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);


        ErrorOr<AuthenticationResult> Login(string email, string password);
       
    }
}