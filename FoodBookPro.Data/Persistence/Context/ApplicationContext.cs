using FoodBookPro.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FoodBookPro.Data.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){ }

        #region DbSets
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region App Configuaration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            #endregion
        }
    }
}
