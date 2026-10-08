using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.Controllers;
using MiniSupermarket.API.Models;
using System.Collections.Generic;
using Xunit;

namespace HeThongBanLeThienAn.Tests
{
    public class CategoriesControllerTests
    {
        private readonly CategoriesController _controller;

        public CategoriesControllerTests()
        {
            _controller = new CategoriesController();
        }

        [Fact]
        public void GetAll_ReturnsOkResultWithListOfCategories()
        {
            // Act
            var result = _controller.GetAll() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var categories = result.Value as List<Category>;
            Assert.NotNull(categories);
            Assert.NotEmpty(categories);
        }

        [Fact]
        public void GetById_ExistingId_ReturnsOkResult()
        {
            // Act
            var result = _controller.GetById(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var category = result.Value as Category;
            Assert.NotNull(category);
            Assert.Equal(1, category.CategoryId);
        }

        [Fact]
        public void GetById_NonExistingId_ReturnsNotFound()
        {
            // Act
            var result = _controller.GetById(9999) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void Search_WithValidKeyword_ReturnsOkResult()
        {
            // Act
            var result = _controller.Search("suối") as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var categories = result.Value as List<Category>;
            Assert.NotNull(categories);
            Assert.Single(categories);
        }

        [Fact]
        public void Search_WithEmptyKeyword_ReturnsBadRequest()
        {
            // Act
            var result = _controller.Search("   ") as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Create_ValidCategory_ReturnsCreatedAtAction()
        {
            // Arrange
            var newCategory = new Category
            {
                CategoryName = "Trà sữa Ô Long",
                Description = "Đồ uống đóng chai",
                StockQuantity = 100
            };

            // Act
            var result = _controller.Create(newCategory) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);

            var createdCat = result.Value as Category;
            Assert.NotNull(createdCat);
            Assert.True(createdCat.CategoryId > 0);
            Assert.Equal("Trà sữa Ô Long", createdCat.CategoryName);
        }

        [Fact]
        public void Create_EmptyName_ReturnsBadRequest()
        {
            // Arrange
            var newCategory = new Category
            {
                CategoryName = "",
                Description = "Đồ uống",
                StockQuantity = 10
            };

            // Act
            var result = _controller.Create(newCategory) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Create_NegativeStock_ReturnsBadRequest()
        {
            // Arrange
            var newCategory = new Category
            {
                CategoryName = "Sản phẩm test",
                Description = "Mô tả",
                StockQuantity = -5
            };

            // Act
            var result = _controller.Create(newCategory) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Update_ExistingCategory_ReturnsNoContent()
        {
            // Arrange
            var updateCategory = new Category
            {
                CategoryName = "Bánh quy bơ giòn",
                Description = "Sản phẩm bánh kẹo cao cấp",
                StockQuantity = 150
            };

            // Act
            var result = _controller.Update(1, updateCategory) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public void Update_NonExistingCategory_ReturnsNotFound()
        {
            // Arrange
            var updateCategory = new Category
            {
                CategoryName = "Không tồn tại",
                Description = "Mô tả",
                StockQuantity = 10
            };

            // Act
            var result = _controller.Update(9999, updateCategory) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void Delete_ExistingCategory_ReturnsNoContent()
        {
            // Arrange - Create a category first to delete
            var newCategory = new Category
            {
                CategoryName = "Sản phẩm để xóa",
                Description = "Mô tả",
                StockQuantity = 10
            };
            var createResult = _controller.Create(newCategory) as CreatedAtActionResult;
            var createdCategory = createResult!.Value as Category;

            // Act
            var deleteResult = _controller.Delete(createdCategory!.CategoryId) as NoContentResult;

            // Assert
            Assert.NotNull(deleteResult);
            Assert.Equal(204, deleteResult.StatusCode);
        }

        [Fact]
        public void Delete_NonExistingCategory_ReturnsNotFound()
        {
            // Act
            var result = _controller.Delete(9999) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void GetAdminDashboard_ReturnsOkResult()
        {
            // Act
            var result = _controller.GetAdminDashboard() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetStaffPos_ReturnsOkResult()
        {
            // Act
            var result = _controller.GetStaffPos() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
