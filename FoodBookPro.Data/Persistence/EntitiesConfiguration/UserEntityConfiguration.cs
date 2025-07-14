using FoodBookPro.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodBookPro.Data.Persistence.EntitiesConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region Basic Confituration
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
            #endregion

            #region Relationships
            //User -> Booking
            builder.HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            //User -> Order
            builder.HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            //User -> Review
            builder.HasMany(u => u.Reviews)
                .WithOne(re => re.User)
                .HasForeignKey(re => re.UserId);

            //User -> Notification
            builder.HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId);

            //User -> Restaurant
            builder.HasOne(u => u.Restaurant)
                .WithOne(r => r.Owner)
                .HasForeignKey<Restaurant>(r => r.OwnerId);
            #endregion
        }
    }
}
