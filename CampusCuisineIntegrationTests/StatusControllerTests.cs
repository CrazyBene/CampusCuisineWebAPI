using Microsoft.AspNetCore.Mvc.Testing;

namespace CampusCuisineIntegrationTests
{
    public class StatusControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;

        public StatusControllerTests(WebApplicationFactory<Program> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async void GetStatus_Success()
        {
            // Arrange
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("/");

            // Assert
            var expected = """{"status":"Server is online."}""";
            var actual = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.Equal(expected, actual);
        }

    }
}