using FoodBookPro.Data.Application.Enums;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Persistence.Context;
using FoodBookPro.Data.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodBookPro.PersistenceTest.FeaturesForOwnersTests
{
    public class UnitTestOrder
    {
        private ApplicationContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: $"BdTest.{Guid.NewGuid().ToString()}")
                .Options;
            return new ApplicationContext(options);
        }

        private async Task SeedOrders(ApplicationContext context)
        {
            var menuItem = new MenuItem { ItemId = 1, Name = "Burger", Price = 550, Description = "Spicy ramen with pork belly, eggs, vegetables and bacon" };
            var order = new Order
            {
                Id = 1,
                RestaurantId = 10,
                UserId = 1,
                OrderDate = DateTime.Today,
                TotalAmount = 10,
                Status = OrderStatus.Delivered,
                Details = new List<OrderDetails>
                {
                    new OrderDetails
                    {
                        MenuItem = menuItem,
                        Quantity = 2,
                        MenuItemId = menuItem.ItemId
                    }
                }
            };

            await context.MenuItems.AddAsync(menuItem);
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetDailyOrderCount_ShouldFail_WhenRestaurantIdInvalid()
        {
            var context = GetInMemoryDbContext();
            var repo = new OrderRepository(context);

            var result = await repo.GetDailyOrderCount(0, DateTime.Today);

            Assert.False(result.IsSuccess);
            Assert.Equal(0, result.Data);
            Assert.Equal("The id cannot be zero or minor than zero", result.Message);
        }

        [Fact]
        public async Task GetDailyOrderCount_ShouldReturnZero_WhenNoOrders()
        {
            var context = GetInMemoryDbContext();
            var repo = new OrderRepository(context);

            var result = await repo.GetDailyOrderCount(99, DateTime.Today);

            Assert.True(result.IsSuccess);
            Assert.Equal(0, result.Data);
            Assert.Equal("This restaurant does not have any orders placed on the selected day", result.Message);
        }

        [Fact]
        public async Task GetDailyOrderCount_ShouldReturnCorrectCount_WhenOrdersExist()
        {
            var context = GetInMemoryDbContext();
            await SeedOrders(context);
            var repo = new OrderRepository(context);

            var result = await repo.GetDailyOrderCount(10, DateTime.Today);

            Assert.True(result.IsSuccess);
            Assert.Equal(1, result.Data);
            Assert.Equal("Orders retrieved successfully", result.Message);
        }

        [Fact]
        public async Task GetMostOrderedMenuItems_ShouldFail_WhenRestaurantIdInvalid()
        {
            var context = GetInMemoryDbContext();
            var repo = new OrderRepository(context);

            var result = await repo.GetMostOrderedMenuItems(0);

            Assert.False(result.IsSuccess);
            Assert.Empty(result.Data);
            Assert.Equal("The id cannot be zero or minor than zero", result.Message);
        }

        [Fact]
        public async Task GetMostOrderedMenuItems_ShouldReturnEmpty_WhenNoData()
        {
            var context = GetInMemoryDbContext();
            var repo = new OrderRepository(context);

            var result = await repo.GetMostOrderedMenuItems(99);

            Assert.True(result.IsSuccess);
            Assert.Empty(result.Data);
            Assert.Equal("This restaurant does not have any orders yet", result.Message);
        }

        [Fact]
        public async Task GetMostOrderedMenuItems_ShouldReturnTopItems_WhenDataExists()
        {
            var context = GetInMemoryDbContext();
            await SeedOrders(context);
            var repo = new OrderRepository(context);

            var result = await repo.GetMostOrderedMenuItems(10);

            Assert.True(result.IsSuccess);
            Assert.Single(result.Data);

            var topItem = result.Data.First();
            Assert.Equal("Burger", topItem.ItemName);
            Assert.Equal(2, topItem.TotalSold);
        }

        [Fact]
        public async Task GetMostOrderedMenuItems_ShouldHandleException()
        {
            var context = GetInMemoryDbContext();
            context.Dispose();

            var repo = new OrderRepository(context);
            var result = await repo.GetMostOrderedMenuItems(1);

            Assert.False(result.IsSuccess);
            Assert.StartsWith("An error ocurred retrieving the data", result.Message);
        }

    }
}
