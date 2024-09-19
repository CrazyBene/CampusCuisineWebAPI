var builder = WebApplication.CreateBuilder(args);

var recipes = new Dictionary<int, Recipe>();

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new { Status = "Server is online." }));

app.MapPost("/recipes", (Recipe recipe) =>
{
    recipes.Add(recipe.Id, recipe);

    return Results.Created("", recipe);
});

app.MapGet("/recipes", () => Results.Ok(new { Recipes = recipes.Values.ToList() }));

app.MapGet("/recipes/{id}", (int id) =>
{
    if (!recipes.ContainsKey(id))
    {
        return Results.NotFound();
    }

    return Results.Ok(recipes[id]);
});

app.MapPut("/recipes/{id}", (int id, Recipe recipe) =>
{
    if (!recipes.ContainsKey(id))
    {
        return Results.NotFound();
    }

    recipe.Id = id;
    recipes[recipe.Id] = recipe;

    return Results.Ok(recipes[recipe.Id]);
});

app.MapPatch("/recipes/{id}", (int id, Recipe recipe) =>
{
    if (!recipes.ContainsKey(id))
    {
        return Results.NotFound();
    }

    recipes[recipe.Id].Category = recipe.Category;

    return Results.Ok(recipes[recipe.Id]);
});

app.MapDelete("/recipes/{id}", (int id) =>
{
    if (!recipes.ContainsKey(id))
    {
        return Results.NotFound();
    }

    recipes.Remove(id);

    return Results.NoContent();
});

app.Run();

record class Recipe
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? Ingredients { get; set; }
    public string? Instructions { get; set; }
}
