using FoodBookPro.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodBookPro.Data.Persistence.EntitiesConfiguration
{
    public class BookingEntitiyConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            #region Basic Configuration
            builder.ToTable("Bookings");
            builder.HasKey(b => b.Id);
            #endregion
            #region Relationships
            //Booking -> Table
            builder.HasOne(b => b.Table)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TableId);
            #endregion
        }
    }
}