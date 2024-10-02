using System.Net;
using CampusCuisine.Model;
using Newtonsoft.Json;

namespace CampusCuisineIntegrationTests
{
    public class RecipesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> factory;
        private readonly HttpClient client;

        public RecipesControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            // Arrange
            this.factory = factory;
            client = factory.CreateClient();
        }

        [Fact]
        public async void CreateRecipe_Success()
        {
            // Act
            var expected = new Recipe
            {
                Name = "TestName",
                Category = "TestCategory",
                Ingredients = "TestIngredients",
                Instructions = "TestInstructions"
            };

            var content = JsonContent.Create(expected);

            var response = await client.PostAsync("/recipes", content);
            var actual = JsonConvert.DeserializeObject<Recipe>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode();
            actual.Id = null;
            Assert.Equivalent(expected, actual);
        }

        [Fact]
        public async void CreateRecipe_Fail()
        {
            // Act
            var expected = new Recipe
            {
                Name = "",
                Category = "TestCategory",
                Ingredients = "TestIngredients",
                Instructions = "TestInstructions"
            };

            var content = JsonContent.Create(expected);

            var response = await client.PostAsync("/recipes", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

    }
}