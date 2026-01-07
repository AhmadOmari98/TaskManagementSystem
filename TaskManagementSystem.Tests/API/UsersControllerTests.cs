using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using TaskManagementSystem.API.Controllers;
using TaskManagementSystem.Application.DTOs.Response;
using TaskManagementSystem.Application.Services.Interface;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Tests.API
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();

            _controller = new UsersController(
                logger: NullLogger<UsersController>.Instance,
                userService: _userServiceMock.Object);

            // Fake HttpContext (headers injected by middleware)
            var httpContext = new DefaultHttpContext();
            httpContext.Items["LoggedInUserId"] = 1;
            httpContext.Items["LoggedInUserRole"] = UserRole.Admin;

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        [Fact]
        public async Task GetById_ReturnsOkResult()
        {
            // Arrange
            _userServiceMock
                .Setup(x => x.GetById(1, 1, UserRole.Admin))
                .ReturnsAsync(new UserResponse
                {
                    Id = 1,
                    Email = "admin@test.com"
                });

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var user = Assert.IsType<UserResponse>(okResult.Value);

            Assert.Equal("admin@test.com", user.Email);
        }
    }
}
