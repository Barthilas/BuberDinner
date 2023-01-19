using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Api.Filters;
using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Application.Services.Authentication.Queries;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneOf;

namespace BuberDinner.Api.Controllers
{
    [Route("auth")]
    // [ErrorHandlingFilter]
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationCommandService _authenticationCommandService;
        private readonly IAuthenticationQueryService _authenticationQueryService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IAuthenticationQueryService authQueryService,
            IAuthenticationCommandService authCommandService)
        {
            _logger = logger;
            _authenticationCommandService = authCommandService;
            _authenticationQueryService = authQueryService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            //ErrorOr
            ErrorOr<AuthenticationResult> registerResult = _authenticationCommandService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            return registerResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors)
                // _ => Problem(statusCode: StatusCodes.Status409Conflict, title: "User already exists.")
            );

            #region FluentResult
            // Result<AuthenticationResult> registerResult = _authenticationService.Register(
            //     request.FirstName,
            //     request.LastName,
            //     request.Email,
            //     request.Password);

            // if(registerResult.IsSuccess)
            // {
            //     return Ok(MapAuthResult(registerResult.Value));
            // }

            // var firstError = registerResult.Errors[0];
            // if(firstError is DuplicateEmailError)
            // {
            //     return Problem(statusCode: StatusCodes.Status409Conflict, title: "Email already exists.");
            // }

            // return Problem();

            #endregion

            #region ONE-OF IMPLEMENTATION
            // OneOf<AuthenticationResult, IError> registerResult = _authenticationService.Register(
            //     request.FirstName,
            //     request.LastName,
            //     request.Email,
            //     request.Password);

            // // more readable
            // return registerResult.Match(
            //     authResult => Ok(MapAuthResult(authResult)),
            //     error => Problem(statusCode: (int)error.StatusCode, title: error.ErrorMessage)
            // );

            // if (registerResult.IsT0)
            // {
            //     var authResult = registerResult.AsT0;
            //     AuthenticationResponse response = MapAuthResult(authResult);
            //     return Ok(response);
            // }
            // return Problem(statusCode: StatusCodes.Status409Conflict, title: "Email already exists.");
            #endregion
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationQueryService.Login(
              request.Email,
              request.Password);

            // Specific handle without using ApiController.
            if (authResult.IsError
               && authResult.FirstError == Domain.Common.Errors.Errors.Authentication.InvalidCredentials)
            {
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: authResult.FirstError.Description
                );
            }

            return authResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors)

            );
        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token
            );
        }

    }
}