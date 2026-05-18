using Cameal.Recipe.Api.Infrastructure;
using Cameal.Recipe.Core.Repositories;
using Cameal.Recipe.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Register domain services and repositories
builder.Services.AddSingleton<IRecipeRepository, InMemoryRecipeRepository>();
builder.Services.AddSingleton<IGroceryListService, GroceryListService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors();

// Root endpoint for testing
app.MapGet("/", () => new
{
    Message = "Cameal Recipe API",
    Endpoints = new[]
    {
        "GET /api/recipes",
        "GET /api/recipes/{id}",
        "POST /api/recipes",
        "PUT /api/recipes/{id}",
        "DELETE /api/recipes/{id}",
        "POST /api/recipes/grocery-list",
        "GET /openapi/v1.json"
    }
});

app.MapControllers();

app.Run();

