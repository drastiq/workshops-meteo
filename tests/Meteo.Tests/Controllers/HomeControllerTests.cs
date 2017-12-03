using System.Threading.Tasks;
using FluentAssertions;
using Meteo.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Meteo.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void home_controller_get_should_return_content()
        {
            //Arrange
            var controller = new HomeController(); //SUT

            //Act
            var result = controller.Get() as ContentResult;

            //Assert
            result.Should().NotBeNull();
            result.Content.ShouldBeEquivalentTo("Hello from Meteo API."); 
        }
    }
}