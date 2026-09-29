using CampusCuisine.Data;
using CampusCuisine.Entity;
using CampusCuisine.Errors;
using CampusCuisine.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CampusCuisineUnitTests;

public class RecipeServiceTests
{
    [Fact]
    public async Task GetRecipeById_Success()
    {
        // arrange
        var userId = Guid.Parse("2146e1ff-5884-44c5-a645-4693d884d18a");
        var recipeId = Guid.Parse("2ea4e945-98c1-421c-a6ce-07bcad645a2c");

        var expectedRecipeEntity = new RecipeEntity(
            userId,
            recipeId,
            "TestRecipe",
            "TestCategory",
            "TestIngredients",
            "TestInstructions"
        );

        var userServiceMock = new Mock<IUserService>();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "campus_cuisine")
            .Options;

        var appDbContextMock = new AppDbContext(options);
        appDbContextMock.Recipes.Add(expectedRecipeEntity);

        userServiceMock.Setup(_ => _.GetUserGuid()).Returns(userId);

        var recipeService = new RecipeService(appDbContextMock, userServiceMock.Object);

        // act
        var actual = await recipeService.GetRecipeById(recipeId);

        // assert
        Assert.Equal(expectedRecipeEntity, actual);
    }

    [Fact]
    public async Task GetRecipeById_NotFound()
    {
        // arrange
        var userId = Guid.Parse("2146e1ff-5884-44c5-a645-4693d884d18a");
        var recipeId = Guid.Parse("2ea4e945-98c1-421c-a6ce-07bcad645a2c");

        var userServiceMock = new Mock<IUserService>();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "campus_cuisine")
            .Options;

        var appDbContextMock = new AppDbContext(options);

        userServiceMock.Setup(_ => _.GetUserGuid()).Returns(userId);

        var recipeService = new RecipeService(appDbContextMock, userServiceMock.Object);

        // act
        var act = async () => await recipeService.GetRecipeById(recipeId);

        // assert
        var notFoundException = await Assert.ThrowsAsync<NotFoundException>(act);
        Assert.Equal($"Recipe Id {recipeId} does not exist!", notFoundException.ErrorMessage);
    }

    [Fact]
    public async Task DeleteRecipe_Forbidden()
    {
        // arrange
        var ownerId = Guid.Parse("2146e1ff-5884-44c5-a645-4693d884d18a");
        var otherUserId = Guid.Parse("7c1b8f2e-3d4a-4f5b-9e6c-1a2b3c4d5e6f");
        var recipeId = Guid.Parse("5b9e825b-67d3-4c3b-8220-a887648f29cd");

        var recipeEntity = new RecipeEntity(
            ownerId,
            recipeId,
            "TestRecipe",
            "TestCategory",
            "TestIngredients",
            "TestInstructions"
        );

        var userServiceMock = new Mock<IUserService>();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "campus_cuisine_forbidden")
            .Options;

        var appDbContextMock = new AppDbContext(options);
        appDbContextMock.Recipes.Add(recipeEntity);

        userServiceMock.Setup(_ => _.GetUserGuid()).Returns(otherUserId);

        var recipeService = new RecipeService(appDbContextMock, userServiceMock.Object);

        // act
        var act = async () => await recipeService.DeleteRecipe(recipeId);

        // assert
        var forbiddenException = await Assert.ThrowsAsync<ForbiddenException>(act);
        Assert.Equal($"Recipe Id {recipeId} belongs to another user!", forbiddenException.ErrorMessage);
    }
}
