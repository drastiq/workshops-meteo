using System.Threading.Tasks;
using FluentAssertions;
using Meteo.Api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Meteo.Tests.EndToEnd.Controllers
{
    public class HomeControllerTests : TestControllerBase
    {
        [Fact]
        public async Task home_controller_get_should_return_content()
        {
            var response = await GetAsync("/");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            content.Should().BeEquivalentTo("Hello from Meteo API.");
        }
    }
}