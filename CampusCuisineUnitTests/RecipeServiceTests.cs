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
    public async void GetRecipeById_Success()
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
    public async void GetRecipeById_NotFound()
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
}