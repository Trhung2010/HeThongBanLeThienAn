using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MiniSupermarket.API.Controllers;
using System.Collections.Generic;
using Xunit;

namespace HeThongBanLeThienAn.Tests
{
    public class AuthControllerTests
    {
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            var myConfiguration = new Dictionary<string, string?>
            {
                {"JwtSettings:Secret", "SupermarketSecretKeyDoAnMonHoc2026SecureString!!"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            _authController = new AuthController(configuration);
        }

        [Fact]
        public void Login_WithAdminCredentials_ReturnsOkWithTokenAndRole()
        {
            // Arrange
            var request = new LoginRequestDto
            {
                Username = "admin",
                Password = "123456"
            };

            // Act
            var result = _authController.Login(request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            dynamic value = result.Value!;
            string role = value.GetType().GetProperty("role").GetValue(value, null);
            bool success = value.GetType().GetProperty("success").GetValue(value, null);
            string token = value.GetType().GetProperty("token").GetValue(value, null);

            Assert.True(success);
            Assert.Equal("Admin", role);
            Assert.False(string.IsNullOrEmpty(token));
        }

        [Fact]
        public void Login_WithCashierCredentials_ReturnsOkWithTokenAndRole()
        {
            // Arrange
            var request = new LoginRequestDto
            {
                Username = "cashier",
                Password = "123456"
            };

            // Act
            var result = _authController.Login(request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            dynamic value = result.Value!;
            string role = value.GetType().GetProperty("role").GetValue(value, null);
            bool success = value.GetType().GetProperty("success").GetValue(value, null);

            Assert.True(success);
            Assert.Equal("Cashier", role);
        }

        [Fact]
        public void Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var request = new LoginRequestDto
            {
                Username = "wronguser",
                Password = "wrongpassword"
            };

            // Act
            var result = _authController.Login(request) as UnauthorizedObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }
    }
}
