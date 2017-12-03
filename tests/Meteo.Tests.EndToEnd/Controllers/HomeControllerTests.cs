using System.Threading.Tasks;
using FluentAssertions;
using Meteo.Api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Meteo.Tests.EndToEnd.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task home_controller_get_should_return_content()
        {
            //Arrange
            var testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
            var client = testServer.CreateClient();

            //Act
            var response = await client.GetAsync("/");

            //Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            content.Should().BeEquivalentTo("Hello from Meteo API.");
        }
    }
}