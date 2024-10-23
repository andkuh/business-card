using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessCard;
using BusinessCard.Employments.Endpoints;
using BusinessCard.Employments.Services;
using BusinessCard.Employments.UseCases.GetEmployments;
using BusinessCard.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Router;
using Router.Configuration;
using Router.Configuration.Formatting;
using Router.Data.EntityFramework;
using Router.Data.Extension;
using Router.DependencyInjection.BuiltIn;
using Router.ErrorHandling;
using Router.ErrorHandling.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddSingleton<IModelToDtoMapper, ModelToDtoMapper>();
builder.Services.AddScoped<IGetEmploymentsQueryHandler, GetEmploymentsQueryHandler>();
builder.Services.AddScoped<IGetEmployments, IGetEmployments.Impl>();

builder.Services.AddApiExpress(s =>
{
    s.AddEndpoints(a => a.UseJson<CamelCasePropertyNamesContractResolver>(j => j.Use<StringEnumConverter>()))
        .AddErrorHandling(err => err.Add<ErrorResponse, Errors>())
        .AddData<Data>
        (
            data =>
                data.UseDbContext<Ctx>(context => { context.UseSqlite("Data source=BusinessCard.db"); })
        );
});

builder.Services.AddHealthChecks().AddCheck<DbCheck>("db")
    .AddCheck<SeedCheck>("db seed");

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(() =>
{
    Task.Run(async () =>
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var ctx = serviceScope.ServiceProvider.GetRequiredService<Ctx>();

            await Seeder.SeedAsync(ctx);
        }
    });
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseApiExpress();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = (async (context, report) =>
    {
        var response = new
        {
            status = report.Status,
            duration = report.TotalDuration,
            entries = report.Entries.Select(s => new
            {
                s.Value.Data,
                s.Value.Description,
                s.Key,
                exception = s.Value.Exception != null
                    ? new
                    {
                        message = s.Value.Exception.Message,
                        innerException = s.Value.Exception.InnerException?.Message
                    }
                    : null,
                s.Value.Duration
            })
        };

        await context.Response.WriteAsJsonAsync(response);
    })
});

app.UseEndpoints(s => s.MapControllers());

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("api"))
    {
        context.Response.StatusCode = 404;
        return;
    }

    await next(context);
});


app.MapFallbackToFile("index.html");


app.Run();