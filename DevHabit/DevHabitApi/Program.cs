using DevHabitApi;
using DevHabitApi.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder
    .AddController()
    .AddErrorHandeling()
    .AddDatabase()
    .AddObservability()
    .AddApplicationServices();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    await app.ApplyMigrationsAsync();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.MapControllers();

await app.RunAsync();
