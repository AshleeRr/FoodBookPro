using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Persistence.Context;
using FoodBookPro.Data.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodBookPro.PersistenceTest.FeaturesForOwnersTests
{
    public class UnitTestNotification
    {
        private ApplicationContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: $"BdTest.{Guid.NewGuid().ToString()}")
                .Options;
            return new ApplicationContext(options);
        }
        private async Task SeedNotification(ApplicationContext context)
        {
            var notification = new Notification
            {
                Id = 1,
                UserId = 100,
                Message = "Test notification",
                NotificationDate = DateTime.Now,
                IsRead = false,
                User = new User { Id = 100, FirstName = "Petroclo", LastName = "Bills", Email = "petrobills@gmail.com", Password = "petronilobonito12#", UserName = "PetBills" }
            };

            await context.Notifications.AddAsync(notification);
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task MarkNotificationAsRead_ShouldFail_WhenIdIsInvalid()
        {
            var context = GetInMemoryDbContext();
            var repo = new NotificationRepository(context);

            var result = await repo.MarkNotificationAsRead(0);

            Assert.False(result.IsSuccess);
            Assert.Equal("The id cannot be zero or minor than zero", result.Message);
        }

        [Fact]
        public async Task MarkNotificationAsRead_ShouldReturnFalse_WhenNotificationNotFound()
        {
            var context = GetInMemoryDbContext();
            var repo = new NotificationRepository(context);

            var result = await repo.MarkNotificationAsRead(999);

            Assert.True(result.IsSuccess);
            Assert.False(result.Data);
            Assert.Equal("It does not exists a notification with this id", result.Message);
        }

        [Fact]
        public async Task MarkNotificationAsRead_ShouldReturnFalse_WhenAlreadyRead()
        {
            var context = GetInMemoryDbContext();
            await SeedNotification(context);

            var notification = await context.Notifications.FindAsync(1);
            notification!.IsRead = true;
            await context.SaveChangesAsync();

            var repo = new NotificationRepository(context);
            var result = await repo.MarkNotificationAsRead(1);

            Assert.True(result.IsSuccess);
            Assert.False(result.Data);
            Assert.Equal("This notification is already marked as read", result.Message);
        }

        [Fact]
        public async Task MarkNotificationAsRead_ShouldMarkAsRead_WhenNotificationIsUnread()
        {
            var context = GetInMemoryDbContext();
            await SeedNotification(context);

            var repo = new NotificationRepository(context);
            var result = await repo.MarkNotificationAsRead(1);

            Assert.True(result.IsSuccess);
            Assert.True(result.Data);
            Assert.Equal("Notification marked as read successfully", result.Message);

            var updatedNotification = await context.Notifications.FindAsync(1);
            Assert.True(updatedNotification!.IsRead);
        }

        [Fact]
        public async Task MarkNotificationAsRead_ShouldHandleException()
        {
            var context = GetInMemoryDbContext();

            var repo = new NotificationRepository(context);
            var result = await repo.MarkNotificationAsRead(1);

            Assert.True(result.IsSuccess);
            Assert.Equal("It does not exists a notification with this id", result.Message);
        }
    }
}
