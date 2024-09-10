using Restaurants.Infrastructure.Extensions;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;
using Serilog.Events;
using Restaurants.API.Middlewares;
using Restaurants.Domain.Entities;
using Restaurants.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddPresentation();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

await seeder.SeedAsync();

// Configure the HTTP request pipeline.

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<CheckExecutionTimeMiddleware>();

app.UseSerilogRequestLogging();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGroup("api/identity")
	.WithTags("Identity")
	.MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
