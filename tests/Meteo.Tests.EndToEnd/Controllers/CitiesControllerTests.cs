using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Meteo.Core.Commands;
using Xunit;

namespace Meteo.Tests.EndToEnd.Controllers
{
    public class CitiesControllerTests : TestControllerBase
    {
        [Fact]
        public async Task cities_controller_post_should_return_created_status_code()
        {
            var response = await PostAsync("cities", new AddCity { Name = "krakow"} );

            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ShouldBeEquivalentTo("cities");
        }        
    }
}