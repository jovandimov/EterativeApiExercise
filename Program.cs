using BlogApi.Core.Interfaces;
using BlogApi.Infrastructure.Data;
using BlogApi.Infrastructure.Migrations;
using BlogApi.Settings;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();


builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection(nameof(MongoDbSettings))
);

builder.Services.AddScoped<IBlogRepository, BlogRepository>();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var options = services.GetRequiredService<IOptions<MongoDbSettings>>();
    var migration = new InitializeDatabase(options.Value);
    await migration.Run();
}

app.Run();
