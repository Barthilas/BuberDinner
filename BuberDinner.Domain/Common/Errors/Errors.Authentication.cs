using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace BuberDinner.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authentication{
            public static Error InvalidCredentials = Error.Validation(
                code: "Auth.InvalidCreds",
                description: "Invalid Credentials.");
        }
        
    }
}