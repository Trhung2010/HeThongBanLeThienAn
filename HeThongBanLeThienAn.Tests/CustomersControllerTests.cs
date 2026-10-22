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
    public class CustomersControllerTests
    {
        private SupermarketDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<SupermarketDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new SupermarketDbContext(options);
            context.Database.EnsureCreated();
            CustomerFixtures.Add(context);
            return context;
        }

        [Fact]
        public async Task GetAll_ReturnsOkResultWithListOfCustomers()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CustomersController(context);

            // Act
            var result = await controller.GetAll() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var customers = result.Value as List<Customer>;
            Assert.NotNull(customers);
            Assert.Equal(15, customers.Count); // 15 seeded customers
        }

        [Fact]
        public async Task GetPaged_OrdersCustomersByRewardPointsDescending()
        {
            using var context = GetDbContext();
            var controller = new CustomersController(context);

            var action = await controller.GetPaged(1, 5);
            var result = Assert.IsType<OkObjectResult>(action.Result);
            var page = Assert.IsType<PagedResult<Customer>>(result.Value);

            Assert.Equal(15, page.TotalItems);
            Assert.Equal(3, page.TotalPages);
            Assert.Equal(5, page.Items.Count);
            Assert.True(page.Items.Zip(page.Items.Skip(1), (left, right) => left.RewardPoints >= right.RewardPoints).All(x => x));
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CustomersController(context);

            // Act
            var result = await controller.GetById(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var customer = result.Value as Customer;
            Assert.NotNull(customer);
            Assert.Equal("Khách mẫu 01", customer.CustomerName);
        }

        [Fact]
        public async Task GetById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CustomersController(context);

            // Act
            var result = await controller.GetById(9999) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task Search_WithValidKeyword_ReturnsMatchingCustomers()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CustomersController(context);

            // Act
            var result = await controller.Search("Khách mẫu") as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var list = result.Value as List<Customer>;
            Assert.NotNull(list);
            Assert.NotEmpty(list);
            Assert.Contains(list, c => c.CustomerName.Contains("Khách mẫu"));
        }

        [Fact]
        public async Task Search_WithEmptyKeyword_ReturnsBadRequest()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CustomersController(context);

            // Act
            var result = await controller.Search("   ") as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Create_ValidCustomer_ReturnsCreatedAtAction()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CustomersController(context);

            var newCustomer = new Customer
            {
                CustomerName = "Phạm Văn D",
                PhoneNumber = "0933445566",
                Address = "101 Điện Biên Phủ",
                RewardPoints = 100,
                MembershipRank = "Bạc"
            };

            // Act
            var result = await controller.Create(newCustomer) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);

            var created = result.Value as Customer;
            Assert.NotNull(created);
            Assert.True(created.CustomerId > 0);
            Assert.Equal("Phạm Văn D", created.CustomerName);
        }

        [Fact]
        public async Task Create_EmptyName_ReturnsBadRequest()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CustomersController(context);

            var newCustomer = new Customer
            {
                CustomerName = "",
                PhoneNumber = "0912345678"
            };

            // Act
            var result = await controller.Create(newCustomer) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Update_ExistingCustomer_ReturnsNoContent()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CustomersController(context);

            var updateData = new Customer
            {
                CustomerName = "Khách mẫu 01 Updated",
                PhoneNumber = "0903123456",
                Address = "Địa chỉ mới",
                RewardPoints = 600,
                MembershipRank = "Bạch Kim"
            };

            // Act
            var result = await controller.Update(1, updateData) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);

            var updatedCustomer = await context.Customers.FindAsync(1);
            Assert.Equal("Khách mẫu 01 Updated", updatedCustomer!.CustomerName);
            Assert.Equal("Bạch Kim", updatedCustomer.MembershipRank);
        }

        [Fact]
        public async Task Delete_ExistingCustomer_ReturnsNoContent()
        {
            // Arrange
            using var context = GetDbContext();
            var controller = new CustomersController(context);

            // Act
            var result = await controller.Delete(1) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);

            var deleted = await context.Customers.FindAsync(1);
            Assert.Null(deleted);
        }
    }
}
