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
    public class ProductsControllerTests
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
        public async Task GetAll_ReturnsOkResultWithListOfProducts()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new ProductsController(context);

            // Act
            var result = await controller.GetAll() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var products = result.Value as List<Product>;
            Assert.NotNull(products);
            Assert.NotEmpty(products);
        }

        [Fact]
        public async Task GetPagedProducts_ReturnsMetadataAndCategoryName()
        {
            using var context = GetDbContext();
            var controller = new ProductsController(context);

            var action = await controller.GetPagedProducts(null, null, 1, 5);
            var result = Assert.IsType<OkObjectResult>(action.Result);
            var page = Assert.IsType<PagedResult<ProductItemDto>>(result.Value);

            Assert.Equal(1, page.CurrentPage);
            Assert.Equal(5, page.PageSize);
            Assert.Equal(100, page.TotalItems);
            Assert.Equal(20, page.TotalPages);
            Assert.Equal(5, page.Items.Count);
            Assert.All(page.Items, item => Assert.False(string.IsNullOrWhiteSpace(item.CategoryName)));
        }

        [Fact]
        public async Task GetPagedProducts_FiltersKeywordAndCategory()
        {
            using var context = GetDbContext();
            var controller = new ProductsController(context);

            var action = await controller.GetPagedProducts("coca", 2, 1, 5);
            var result = Assert.IsType<OkObjectResult>(action.Result);
            var page = Assert.IsType<PagedResult<ProductItemDto>>(result.Value);

            Assert.Equal(1, page.TotalItems);
            var product = Assert.Single(page.Items);
            Assert.Contains("Coca", product.ProductName);
            Assert.Equal(2, product.CategoryId);
            Assert.False(string.IsNullOrWhiteSpace(product.CategoryName));
        }

        [Fact]
        public async Task GetPagedProducts_NormalizesInvalidPagingValues()
        {
            using var context = GetDbContext();
            var controller = new ProductsController(context);

            var action = await controller.GetPagedProducts(null, null, 0, 500);
            var result = Assert.IsType<OkObjectResult>(action.Result);
            var page = Assert.IsType<PagedResult<ProductItemDto>>(result.Value);

            Assert.Equal(1, page.CurrentPage);
            Assert.Equal(5, page.PageSize);
            Assert.Equal(5, page.Items.Count);
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new ProductsController(context);

            // Act
            var result = await controller.GetById(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var product = result.Value as Product;
            Assert.NotNull(product);
            Assert.Equal(1, product.ProductId);
        }

        [Fact]
        public async Task GetById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new ProductsController(context);

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
            var controller = new ProductsController(context);

            // Act
            var result = await controller.Search("Lavie") as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var products = result.Value as List<Product>;
            Assert.NotNull(products);
            Assert.NotEmpty(products);
        }

        [Fact]
        public async Task Create_ValidProduct_ReturnsCreatedAtAction()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new ProductsController(context);

            var newProduct = new Product
            {
                Barcode = "893999999999",
                ProductName = "Coca Cola Lon 330ml",
                Price = 10000m,
                StockQuantity = 50,
                CategoryId = 2
            };

            // Act
            var result = await controller.Create(newProduct) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);

            var createdProduct = result.Value as Product;
            Assert.NotNull(createdProduct);
            Assert.Equal("Coca Cola Lon 330ml", createdProduct.ProductName);
        }

        [Fact]
        public async Task Update_ExistingProduct_ReturnsNoContent()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new ProductsController(context);

            var updateData = new Product
            {
                Barcode = "893456789001",
                ProductName = "Bánh quy AFC Dinh Dưỡng Cải Tiến",
                Price = 30000m,
                StockQuantity = 80,
                CategoryId = 1
            };

            // Act
            var result = await controller.Update(1, updateData) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);

            var updatedInDb = await context.Products.FindAsync(1);
            Assert.NotNull(updatedInDb);
            Assert.Equal("Bánh quy AFC Dinh Dưỡng Cải Tiến", updatedInDb.ProductName);
            Assert.Equal(30000m, updatedInDb.Price);
        }

        [Fact]
        public async Task Delete_ExistingProduct_ReturnsNoContent()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new ProductsController(context);

            // Act
            var result = await controller.Delete(1) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);

            var deleted = await context.Products.FindAsync(1);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task GetByBarcode_ValidBarcode_ReturnsOkResult()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new ProductsController(context);

            // Act
            var result = await controller.GetByBarcode("893456789006") as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var product = result.Value as Product;
            Assert.NotNull(product);
            Assert.Equal("893456789006", product.Barcode);
            Assert.Contains("Lavie", product.ProductName);
        }
    }
}
