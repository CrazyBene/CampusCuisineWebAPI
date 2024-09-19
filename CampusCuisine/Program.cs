var builder = WebApplication.CreateBuilder(args);

var recipes = new List<Recipe>();

var app = builder.Build();

app.MapGet("/", () => "Hello World");

app.MapPost("/createRecipe", (Recipe recipe) =>
{
    recipes.Add(recipe);
});

app.MapGet("/getAllRecipes", () => recipes);

app.MapGet("/retrieveSingleRecipe/{id}", (int id) =>
{
    if (id >= recipes.Count)
    {
        return null;
    }

    return recipes[id];
});

app.Run();

record Recipe(
    string Name,
    string Category,
    string Ingredients,
    string Instructions
);
