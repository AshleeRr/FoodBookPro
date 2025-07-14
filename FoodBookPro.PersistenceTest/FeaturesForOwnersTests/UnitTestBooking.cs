using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Persistence.Context;
using FoodBookPro.Data.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodBookPro.PersistenceTest.FeaturesForOwnersTests
{
    public class UnitTestBooking
    {
        private ApplicationContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: $"BdTest.{Guid.NewGuid().ToString()}")
                .Options;
            return new ApplicationContext(options);
        }

        private async Task SeedBookings(ApplicationContext context)
        {
            var bookings = new List<Booking>
            {
                new Booking
                {
                    Id = 1,
                    UserId = 10,
                    RestaurantId = 1,
                    TableId = 1,
                    BookingDate = DateOnly.FromDateTime(DateTime.Today),
                    BookingTime = TimeOnly.FromDateTime(DateTime.Now),
                    NumberOfGuests = 2,
                    Status = 1,
                    Restaurant = new Restaurant { Id = 1, Name = "HimikVoice", Address = "Calle La Miranda", Description = "Somos un restarante japones que...", Specialty = "Comida de origen asiatico"},
                    Table = new Table { Id = 1 }
                }
            };
            await context.Bookings.AddRangeAsync(bookings);
            await context.SaveChangesAsync();
        }
        [Fact]
        public async Task GetAllBookingsOfAClient_ShouldReturnBookings_WhenUserHasBookings()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            await SeedBookings(context);

            var repository = new BookingRepository(context);

            // Act
            var result = await repository.GetAllBookingsOfAClient(10);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal("Bookings retrieved successfully", result.Message);
        }
        [Fact]
        public async Task GetAllBookingsOfAClient_ShouldReturnFailure_WhenUserIdIsInvalid()
        {
            var context = GetInMemoryDbContext();
            var repository = new BookingRepository(context);

            var result = await repository.GetAllBookingsOfAClient(0);

            Assert.False(result.IsSuccess);
            Assert.Equal("The id can not have the value zero or minor", result.Message);
        }
        [Fact]
        public async Task GetAllBookingsOfAClient_ShouldReturnEmptyList_WhenUserHasNoBookings()
        {
            var context = GetInMemoryDbContext();
            var repository = new BookingRepository(context);

            var result = await repository.GetAllBookingsOfAClient(999);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
            Assert.Equal("This user does not have bookings yet", result.Message);
        }
        [Fact]
        public async Task GetAllBookingsOfAClient_ShouldHandleException()
        {
            var context = GetInMemoryDbContext();
            context.Dispose(); // esto simula fallo interno

            var repository = new BookingRepository(context);
            var result = await repository.GetAllBookingsOfAClient(1);

            Assert.False(result.IsSuccess);
            Assert.StartsWith("Error retieving the bookings from database", result.Message);
        }

    }
}
