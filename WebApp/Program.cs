using Logic.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using WebApp;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var startup = new Startup();
startup.AddServices(builder);

var app = builder.Build();

// 404 instead of 500
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {

        context.Response.ContentType = Text.Plain;
        // using static System.Net.Mime.MediaTypeNames;

        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();

        if(exceptionHandlerPathFeature?.Error is DuplicationException de)
        {
            context.Response.StatusCode = 409;
            await context.Response.WriteAsync(de.ExceptionMessage());
        }

        else if (exceptionHandlerPathFeature?.Error is NotFoundException nfe)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(nfe.ExceptionMessage());
        }


        else if (exceptionHandlerPathFeature?.Error is ValidationException ve)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(ve.ExceptionMessage());
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsync("An exception was thrown.");

        }
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
