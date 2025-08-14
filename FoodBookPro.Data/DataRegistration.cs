using FoodBookPro.Data.Domain.Interfaces.Common;
using FoodBookPro.Data.Application.Services;
using FoodBookPro.Data.Persistence.Context;
using FoodBookPro.Data.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FoodBookPro.Data.Domain.Interfaces.Repositories;
using FoodBookPro.Data.Domain.Interfaces.Services;
using System.Reflection;

namespace FoodBookPro.Data
{
    public static class DataRegistration
    {
        public static void AddDataIoc(this IServiceCollection services, IConfiguration config)
        {
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(opt => opt.UseInMemoryDatabase("AppDb"));
            }

            else
            {
                var connection = config.GetConnectionString("BorromeConnection");
                services
                    .AddDbContext<ApplicationContext>(opt => 
                        opt.UseSqlServer(connection, m => 
                            m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)), ServiceLifetime.Transient);
            }

            // generic register 
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IMenuItemRepository,MenuItemRepository>();
            services.AddTransient<INotificationRepository,NotificationRepository>();
            services.AddTransient<IOrderRepository,OrderRepository>();
            services.AddTransient<IPaymentRepository,PaymentRepository>();
            services.AddTransient<IRestaurantRepository,RestaurantRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IUserService, UserService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
