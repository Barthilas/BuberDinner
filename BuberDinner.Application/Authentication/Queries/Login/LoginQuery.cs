using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Queries.Login
{
    // Command encapsulates data needed for mediator to proceed.
    public record LoginQuery(
        string Email,
        string Password
    ) : IRequest<ErrorOr<AuthenticationResult>> ;
}