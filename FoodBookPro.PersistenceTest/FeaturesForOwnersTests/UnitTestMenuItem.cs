using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Persistence.Context;
using FoodBookPro.Data.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodBookPro.PersistenceTest.FeaturesForOwnersTests
{
    public class UnitTestMenuItem
    {
        private ApplicationContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: $"BdTest.{Guid.NewGuid().ToString()}")
                .Options;
            return new ApplicationContext(options);
        }

        private async Task SeedMenuItem(ApplicationContext context)
        {
            var menuItem = new MenuItem
            {
                ItemId = 1,
                RestaurantId = 100,
                Name = "Test Pizza",
                Description = "Delicious",
                Price = 10.5m,
                Status = true,
                Restaurant = new Restaurant { Id = 100, Name = "Testaurant", Address="WayToShort", Description="ForMySoul", Specialty="Corazon" }
            };
            await context.MenuItems.AddAsync(menuItem);
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task ChangeMenuItemStatus_ShouldFail_WhenIdIsInvalid()
        {
            var context = GetInMemoryDbContext();
            var repo = new MenuItemRepository(context);

            var result = await repo.ChangeMenuItemStatus(0, false);

            Assert.False(result.IsSuccess);
            Assert.Equal("The id cannot be zero or minor than zero", result.Message);
        }
        [Fact]
        public async Task ChangeMenuItemStatus_ShouldReturnFalse_WhenMenuItemNotFound()
        {
            var context = GetInMemoryDbContext();
            var repo = new MenuItemRepository(context);

            var result = await repo.ChangeMenuItemStatus(999, false);

            Assert.True(result.IsSuccess);
            Assert.False(result.Data);
            Assert.Equal("There is not a menu item with this id in the database", result.Message);
        }
        [Fact]
        public async Task ChangeMenuItemStatus_ShouldUpdateStatus_WhenMenuItemExists()
        {
            var context = GetInMemoryDbContext();
            await SeedMenuItem(context);

            var repo = new MenuItemRepository(context);

            var result = await repo.ChangeMenuItemStatus(1, false);

            Assert.True(result.IsSuccess);
            Assert.True(result.Data);
            Assert.Equal("Menu item status updated successfully", result.Message);

            var updatedItem = await context.MenuItems.FindAsync(1);
            Assert.False(updatedItem!.Status);
        }
        [Fact]
        public async Task ChangeMenuItemStatus_ShouldHandleException()
        {
            var context = GetInMemoryDbContext();
            context.Dispose(); // simulamos un fallo
            var repo = new MenuItemRepository(context);

            var result = await repo.ChangeMenuItemStatus(1, false);

            Assert.False(result.IsSuccess);
            Assert.StartsWith("Error changing the status of the menu item", result.Message);
        }
        [Fact]
        public async Task GetMenuItemsByRestaurant_ShouldFail_WhenRestaurantIdIsInvalid()
        {
            var context = GetInMemoryDbContext();
            var repo = new MenuItemRepository(context);

            var result = await repo.GetMenuItemsByRestaurant(0);

            Assert.False(result.IsSuccess);
            Assert.Equal("The id cannot be zero or minor than zero", result.Message);
        }

        [Fact]
        public async Task GetMenuItemsByRestaurant_ShouldReturnEmpty_WhenNoItemsExist()
        {
            var context = GetInMemoryDbContext();
            var repo = new MenuItemRepository(context);

            var result = await repo.GetMenuItemsByRestaurant(999);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
            Assert.Equal("This restaurant does not contain any menu item", result.Message);
        }

        [Fact]
        public async Task GetMenuItemsByRestaurant_ShouldReturnItems_WhenItemsExist()
        {
            var context = GetInMemoryDbContext();
            await SeedMenuItem(context);

            var repo = new MenuItemRepository(context);

            var result = await repo.GetMenuItemsByRestaurant(100);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal("Menu items retrieved succesfully", result.Message);
        }

        [Fact]
        public async Task GetMenuItemsByRestaurant_ShouldHandleException()
        {
            var context = GetInMemoryDbContext();
            context.Dispose(); // simulamos fallo
            var repo = new MenuItemRepository(context);

            var result = await repo.GetMenuItemsByRestaurant(1);

            Assert.False(result.IsSuccess);
            Assert.StartsWith("Error retrieving the menu items of the restaurant", result.Message);
        }
    }
}
