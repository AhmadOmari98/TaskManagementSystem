using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using TaskManagementSystem.Application.Services.Implementation;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Domain.Interface.Repositories;

namespace TaskManagementSystem.Tests.Application
{
    public class UserServiceTests
    {
        private readonly Mock<IRepository<User>> _userRepoMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepoMock = new Mock<IRepository<User>>();

            _userService = new UserService(
                logger: NullLogger<UserService>.Instance,
                repository: _userRepoMock.Object);
        }

        [Fact]
        public async Task GetById_AdminCanViewAnyUser_ReturnsUser()
        {
            // Arrange
            var user = new User("Test User", "test@test.com", UserRole.User, null);
            typeof(User).GetProperty("Id")!.SetValue(user, 2);

            _userRepoMock
                .Setup(x => x.GetByIdAsync(2))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.GetById(
                id: 2,
                loggedInUserId: 1,
                loggedInUserRole: UserRole.Admin);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("test@test.com", result.Email);
        }

        [Fact]
        public async Task GetById_UserTryingToViewAnotherUser_ThrowsException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                _userService.GetById(
                    id: 2,
                    loggedInUserId: 1,
                    loggedInUserRole: UserRole.User));
        }
    }
}
