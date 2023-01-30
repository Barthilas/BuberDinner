
using BuberDinner.Api;
using BuberDinner.Application;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddInfrastructure(builder.Configuration)
        .AddApplication();
    
    // builder.Services.AddControllers(options =>
    // {
    //     // Second approach of global error handling.
    //     // Add globaly doesnt need to be added to every single each controller/method.
    //     // options.Filters.Add<ErrorHandlingFilterAttribute>();
    // });
    // //Override default problemsFactory for third approach.
    // builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
}


var app = builder.Build();
{

    // First approach to global error handling.
    // app.UseMiddleware<ErrorHandlingMidleware>();

    // Third approach for errors.
    app.UseExceptionHandler("/error");

    // Without controller 
    // app.Map("/error", (HttpContext httpContext) => {
    //     Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    //     return Results.Problem();
    // });

    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}


