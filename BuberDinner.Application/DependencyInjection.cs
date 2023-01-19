using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);
            // Mediator replaced.
            // services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
            // services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
            return services;
        }
    }
}