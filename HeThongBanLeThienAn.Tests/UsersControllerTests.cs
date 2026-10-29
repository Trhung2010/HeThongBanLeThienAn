using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Controllers;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HeThongBanLeThienAn.Tests
{
    public class UsersControllerTests
    {
        private SupermarketDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<SupermarketDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new SupermarketDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public void GetAll_WithDbContext_ReturnsUserDtoList()
        {
            using var context = GetDbContext();
            var controller = new UsersController(context);

            var action = controller.GetAll();
            var result = Assert.IsType<OkObjectResult>(action);
            Assert.Equal(200, result.StatusCode);

            var users = Assert.IsType<List<UserDto>>(result.Value);
            Assert.NotEmpty(users);
            Assert.All(users, u =>
            {
                Assert.True(u.Id > 0);
                Assert.False(string.IsNullOrWhiteSpace(u.Username));
            });
        }

        [Fact]
        public async Task GetPaged_ReturnsPagedUserDtos()
        {
            using var context = GetDbContext();
            var controller = new UsersController(context);

            var action = await controller.GetPaged(null, 1, 5);
            var result = Assert.IsType<OkObjectResult>(action);
            Assert.Equal(200, result.StatusCode);

            var page = Assert.IsType<PagedResult<UserDto>>(result.Value);
            Assert.Equal(1, page.CurrentPage);
            Assert.Equal(5, page.PageSize);
            Assert.Equal(5, page.Items.Count);
        }

        [Fact]
        public async Task GetById_ExistingUser_ReturnsUserDto()
        {
            using var context = GetDbContext();
            var controller = new UsersController(context);

            var action = await controller.GetById(1);
            var result = Assert.IsType<OkObjectResult>(action);
            Assert.Equal(200, result.StatusCode);

            var user = Assert.IsType<UserDto>(result.Value);
            Assert.Equal(1, user.Id);
            Assert.Equal("admin", user.Username);
        }

        [Fact]
        public async Task GetById_NonExistingUser_ReturnsNotFound()
        {
            using var context = GetDbContext();
            var controller = new UsersController(context);

            var action = await controller.GetById(9999);
            var result = Assert.IsType<NotFoundObjectResult>(action);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void Create_ValidUser_ReturnsCreatedAtActionWithUserDto()
        {
            using var context = GetDbContext();
            var controller = new UsersController(context);

            var request = new CreateUserDto
            {
                Username = "new_cashier_test",
                Password = "Password123!",
                FullName = "Nhân viên Mới",
                Role = "Cashier"
            };

            var action = controller.Create(request);
            var result = Assert.IsType<CreatedAtActionResult>(action);
            Assert.Equal(201, result.StatusCode);

            var created = Assert.IsType<UserDto>(result.Value);
            Assert.Equal("new_cashier_test", created.Username);
            Assert.Equal("Nhân viên Mới", created.FullName);
            Assert.Equal("Cashier", created.Role);
            Assert.True(created.IsActive);
        }

        [Fact]
        public void Create_EmptyUsernameOrPassword_ReturnsBadRequest()
        {
            using var context = GetDbContext();
            var controller = new UsersController(context);

            var request = new CreateUserDto
            {
                Username = "",
                Password = ""
            };

            var action = controller.Create(request);
            var result = Assert.IsType<BadRequestObjectResult>(action);
            Assert.Equal(400, result.StatusCode);
        }
    }
}
