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
    public class OrdersControllerTests
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
        public async Task GetAll_ReturnsOkResultWithListOfOrders()
        {
            using var context = GetDbContext();
            var controller = new OrdersController(context, null!);

            var action = await controller.GetAll();
            var result = Assert.IsType<OkObjectResult>(action);
            Assert.Equal(200, result.StatusCode);

            var orders = Assert.IsType<List<OrderDto>>(result.Value);
            Assert.NotEmpty(orders);
            Assert.All(orders, o =>
            {
                Assert.False(string.IsNullOrWhiteSpace(o.OrderCode));
                Assert.NotNull(o.OrderDetails);
            });
        }

        [Fact]
        public async Task GetPaged_ReturnsPagedOrderSummaryDtos()
        {
            using var context = GetDbContext();
            var controller = new OrdersController(context, null!);

            var action = await controller.GetPaged(null, 1, 5);
            var result = Assert.IsType<OkObjectResult>(action);
            Assert.Equal(200, result.StatusCode);

            var page = Assert.IsType<PagedResult<OrderSummaryDto>>(result.Value);
            Assert.Equal(1, page.CurrentPage);
            Assert.Equal(5, page.PageSize);
            Assert.NotEmpty(page.Items);
        }

        [Fact]
        public async Task GetById_ExistingOrder_ReturnsOrderDtoWithDetails()
        {
            using var context = GetDbContext();
            var controller = new OrdersController(context, null!);

            var action = await controller.GetById(1);
            var result = Assert.IsType<OkObjectResult>(action);
            Assert.Equal(200, result.StatusCode);

            var order = Assert.IsType<OrderDto>(result.Value);
            Assert.Equal(1, order.OrderId);
            Assert.NotEmpty(order.OrderDetails);
        }

        [Fact]
        public async Task GetById_NonExistingOrder_ReturnsNotFound()
        {
            using var context = GetDbContext();
            var controller = new OrdersController(context, null!);

            var action = await controller.GetById(9999);
            var result = Assert.IsType<NotFoundObjectResult>(action);
            Assert.Equal(404, result.StatusCode);
        }
    }
}
