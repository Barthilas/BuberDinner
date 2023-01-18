
using BuberDinner.Api.Errors;
using BuberDinner.Api.Filters;
using BuberDinner.Api.Middleware;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddInfrastructure(builder.Configuration)
        .AddApplication();
    
    builder.Services.AddControllers(options =>
    {
        // Second approach of global error handling.
        // Add globaly doesnt need to be added to every single each controller/method.
        // options.Filters.Add<ErrorHandlingFilterAttribute>();
    });
    //Override default problemsFactory.
    builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
}


var app = builder.Build();
{

    // First approach to global error handling.
    // app.UseMiddleware<ErrorHandlingMidleware>();

    // Third approach for errors.
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}


