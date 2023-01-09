using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication
{
    public record AuthenticationResult
    (
        Guid Id,
        string FirstNAme,
        string LastName,
        string Email,
        string Token
    );
}