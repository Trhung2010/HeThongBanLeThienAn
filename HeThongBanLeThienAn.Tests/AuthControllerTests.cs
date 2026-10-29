using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MiniSupermarket.API.Controllers;
using MiniSupermarket.API.Models;
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

            var value = Assert.IsType<LoginResponseDto>(result.Value);

            Assert.True(value.Success);
            Assert.Equal("Admin", value.Role);
            Assert.False(string.IsNullOrEmpty(value.Token));
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

            var value = Assert.IsType<LoginResponseDto>(result.Value);

            Assert.True(value.Success);
            Assert.Equal("Cashier", value.Role);
        }

        [Fact]
        public void Login_WithWarehouseCredentials_ReturnsOkWithTokenAndRole()
        {
            // Arrange
            var request = new LoginRequestDto
            {
                Username = "ware01",
                Password = "123456"
            };

            // Act
            var result = _authController.Login(request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var value = Assert.IsType<LoginResponseDto>(result.Value);

            Assert.True(value.Success);
            Assert.Equal("Warehouse", value.Role);
            Assert.Equal("ware01", value.Username);
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
