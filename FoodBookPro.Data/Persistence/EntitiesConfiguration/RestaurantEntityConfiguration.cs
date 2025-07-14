using FoodBookPro.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodBookPro.Data.Persistence.EntitiesConfiguration
{
    public class RestaurantEntityConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            #region Basic Configuration
            builder.ToTable("Restaurants");
            builder.HasKey(r => r.Id);
            #endregion

            #region Relationships
            //Restaurant -> Tables
            builder.HasMany(r => r.Tables)
                .WithOne(t => t.Restaurant)
                .HasForeignKey(t => t.RestaurantId);

            //Restaurant -> Booking
            builder.HasMany(r => r.Bookings)
                .WithOne(b => b.Restaurant)
                .HasForeignKey(b => b.RestaurantId);

            //Restaurant -> MenuItems
            builder.HasMany(r => r.MenuItems)
                .WithOne(m => m.Restaurant)
                .HasForeignKey(m => m.RestaurantId);

            //Restaurant -> Orders
            builder.HasMany(r => r.Orders)
                .WithOne(o => o.Restaurant)
                .HasForeignKey(o => o.RestaurantId);

            //Restaurant -> Review
            builder.HasMany(r => r.Reviews)
                .WithOne(re => re.Restaurant)
                .HasForeignKey(re => re.RestaurantId);
            #endregion
        }
    }
}
