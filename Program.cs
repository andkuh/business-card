using BusinessCard;
using BusinessCard.Infrastructure;
using BusinessCard.People.Endpoints;
using BusinessCard.People.Records;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Router;
using Router.Configuration;
using Router.Configuration.Formatting;
using Router.Contracts;
using Router.Data.Configuration;
using Router.Data.EntityFramework;
using Router.Data.Extension;
using Router.DependencyInjection.BuiltIn;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

//builder.Services.AddDbContext<Ctx>(s => s.UseInMemoryDatabase("Context"));

builder.Services.AddApiExpress(s =>
{
    s.AddEndpoints(a => a.UseJson<CamelCasePropertyNamesContractResolver>())
        .AddData<Data>
        (
            data =>
                data.UseDbContext<Ctx>(context =>
                {
                    //context.UseSqlServer(builder.Configuration.GetConnectionString("BusinessCard"));
                    context.UseInMemoryDatabase("BusinessCard");
                })
        );
});

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(() =>
{
    var serviceScope = app.Services.CreateScope();

    var ctx = serviceScope.ServiceProvider.GetRequiredService<Ctx>();

    Seeder.Seed(ctx);
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