using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using University.API.Controllers;
using University.Domain.Entity.User.Responses;
using University.Domain.Responses;
using University.Domain.Services;

namespace University.Tests
{
    [TestClass]
    public class InsertDataTest
    {
        [TestMethod]
        public async Task InsertDataTest()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var logger = Mock.Of<ILogger<UserController>>();
            var controller = new UserController(mockService.Object, logger);
            var request = new InsertUserRequest
            {
                Name = "Test",
                Email = ""
            }
        }
    }
}