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
    public class CategoriesControllerTests
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
        public async Task GetAll_ReturnsOkResultWithListOfCategories()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CategoriesController(context);

            // Act
            var result = await controller.GetAll() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var categories = result.Value as List<Category>;
            Assert.NotNull(categories);
            Assert.NotEmpty(categories);
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CategoriesController(context);

            // Act
            var result = await controller.GetById(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var category = result.Value as Category;
            Assert.NotNull(category);
            Assert.Equal(1, category.CategoryId);
        }

        [Fact]
        public async Task GetById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CategoriesController(context);

            // Act
            var result = await controller.GetById(9999) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task Search_WithValidKeyword_ReturnsOkResult()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CategoriesController(context);

            // Act
            var result = await controller.Search("suối") as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var categories = result.Value as List<Category>;
            Assert.NotNull(categories);
            Assert.Single(categories);
        }

        [Fact]
        public async Task Search_WithEmptyKeyword_ReturnsBadRequest()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CategoriesController(context);

            // Act
            var result = await controller.Search("   ") as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Create_ValidCategory_ReturnsCreatedAtAction()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CategoriesController(context);

            var newCategory = new Category
            {
                CategoryName = "Trà sữa Ô Long",
                Description = "Đồ uống đóng chai",
                StockQuantity = 100
            };

            // Act
            var result = await controller.Create(newCategory) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);

            var createdCat = result.Value as Category;
            Assert.NotNull(createdCat);
            Assert.True(createdCat.CategoryId > 0);
            Assert.Equal("Trà sữa Ô Long", createdCat.CategoryName);
        }

        [Fact]
        public async Task Create_EmptyName_ReturnsBadRequest()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CategoriesController(context);

            var newCategory = new Category
            {
                CategoryName = "",
                Description = "Đồ uống",
                StockQuantity = 10
            };

            // Act
            var result = await controller.Create(newCategory) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Update_ExistingCategory_ReturnsNoContent()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CategoriesController(context);

            var updateCategory = new Category
            {
                CategoryName = "Bánh quy bơ giòn",
                Description = "Sản phẩm bánh kẹo cao cấp",
                StockQuantity = 150
            };

            // Act
            var result = await controller.Update(1, updateCategory) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task Delete_ExistingCategory_ReturnsNoContent()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CategoriesController(context);

            // Act
            var result = await controller.Delete(1) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public void GetAdminDashboard_ReturnsOkResult()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CategoriesController(context);

            // Act
            var result = controller.GetAdminDashboard() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetStaffPos_ReturnsOkResult()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CategoriesController(context);

            // Act
            var result = controller.GetStaffPos() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
