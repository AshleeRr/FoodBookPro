using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Persistence.Context;
using FoodBookPro.Data.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodBookPro.PersistenceTest.FeaturesForOwnersTests
{
    public class UnitTestPayment
    {
        private ApplicationContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: $"BdTest.{Guid.NewGuid().ToString()}")
                .Options;
            return new ApplicationContext(options);
        }
        private async Task SeedPayments(ApplicationContext context)
        {
            var order = new Order
            {
                Id = 1,
                UserId = 101,
                RestaurantId = 1,
                OrderDate = DateTime.Now,
                Status = Data.Application.Enums.OrderStatus.Delivered,
                TotalAmount = 50
            };

            var payment = new Payment
            {
                Id = 1,
                OrderId = 1,
                Order = order,
                Amount = 50,
                PaymentDate = DateTime.Now,
                PaymentMethod = "Credit Card",
                Status = true
            };

            await context.Orders.AddAsync(order);
            await context.Payments.AddAsync(payment);
            await context.SaveChangesAsync();
        }

        // SaveFrecuentPaymentMethod Tests
        [Fact]
        public async Task SaveFrecuentPaymentMethod_ShouldFail_WhenUserIdIsInvalid()
        {
            var context = GetInMemoryDbContext();
            var repo = new PaymentRepository(context);

            var result = await repo.SaveFrecuentPaymentMethod(0, "Card");

            Assert.False(result.IsSuccess);
            Assert.Equal("The id cannot be zero or minor than zero", result.Message);
        }

        [Fact]
        public async Task SaveFrecuentPaymentMethod_ShouldFail_WhenPaymentMethodIsEmpty()
        {
            var context = GetInMemoryDbContext();
            var repo = new PaymentRepository(context);

            var result = await repo.SaveFrecuentPaymentMethod(1, "");

            Assert.False(result.IsSuccess);
            Assert.Equal("The payment method must not be empty", result.Message);
        }

        [Fact]
        public async Task SaveFrecuentPaymentMethod_ShouldSave_WhenValid()
        {
            var context = GetInMemoryDbContext();
            var repo = new PaymentRepository(context);

            var result = await repo.SaveFrecuentPaymentMethod(1, "Cash");

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("Cash", result.Data.PaymentMethod);
        }

        [Fact]
        public async Task SaveFrecuentPaymentMethod_ShouldHandleException()
        {
            var context = GetInMemoryDbContext();
            context.Dispose();

            var repo = new PaymentRepository(context);
            var result = await repo.SaveFrecuentPaymentMethod(1, "Cash");

            Assert.False(result.IsSuccess);
            Assert.StartsWith("Error trying to save the payment mehod", result.Message);
        }
        
        [Fact]
        public async Task GetFrecuentPaymentMethods_ShouldFail_WhenUserIdInvalid()
        {
            var context = GetInMemoryDbContext();
            var repo = new PaymentRepository(context);

            var result = await repo.GetFrecuentPaymentMethods(0);

            Assert.False(result.IsSuccess);
            Assert.Equal("The id cannot be zero or minor than zero", result.Message);
        }

        [Fact]
        public async Task GetFrecuentPaymentMethods_ShouldReturnEmpty_WhenNoPayments()
        {
            var context = GetInMemoryDbContext();
            var repo = new PaymentRepository(context);

            var result = await repo.GetFrecuentPaymentMethods(123);

            Assert.True(result.IsSuccess);
            Assert.Empty(result.Data);
            Assert.Equal("This user does not have any current payment methods saved", result.Message);
        }

        [Fact]
        public async Task GetFrecuentPaymentMethods_ShouldReturnPayments_WhenExist()
        {
            var context = GetInMemoryDbContext();
            await SeedPayments(context);
            var repo = new PaymentRepository(context);

            var result = await repo.GetFrecuentPaymentMethods(101);

            Assert.True(result.IsSuccess);
            Assert.Single(result.Data);
            Assert.Equal("Credit Card", result.Data.First().PaymentMethod);
        }

        [Fact]
        public async Task GetFrecuentPaymentMethods_ShouldHandleException()
        {
            var context = GetInMemoryDbContext();
            context.Dispose();

            var repo = new PaymentRepository(context);
            var result = await repo.GetFrecuentPaymentMethods(1);

            Assert.False(result.IsSuccess);
            Assert.StartsWith("Error retieving the payment method/s from database", result.Message);
        }


    }
}
