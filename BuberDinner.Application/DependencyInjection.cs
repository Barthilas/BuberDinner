using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Common.Behaviours;
using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Application.Services.Authentication.Queries;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);
            
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehaviour<,>));

            // services.AddScoped<
            // IPipelineBehavior
            // < RegisterCommand, ErrorOr<AuthenticationResult>>, 
            // ValidateRegisterCommandBehaviour>();    
            // Detect validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            // services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();
            
            // Mediator replaced.
            // services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
            // services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
            return services;
        }
    }
}