using System;
using System.Security.Claims;
using FluentAssertions;
using Meteo.Api.Controllers;
using Meteo.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Meteo.Tests.Controllers
{
    public class AccountControllerTests
    {
        [Fact]
        public void account_controller_get_should_return_content()
        {
            var userServiceMock = new Mock<IUserService>();
            var controller = new AccountController(userServiceMock.Object);
            var userId = Guid.NewGuid().ToString();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(
                        new Claim[] { new Claim(ClaimTypes.Name, userId)}
                    , "test"))
                }
            };

            var result = controller.Get() as ContentResult;

            result.Should().NotBeNull();
            result.Content.Should().BeEquivalentTo($"Hello {userId}");
        }
    }
}