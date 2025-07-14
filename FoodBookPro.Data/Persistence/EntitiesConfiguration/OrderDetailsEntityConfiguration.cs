using FoodBookPro.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodBookPro.Data.Persistence.EntitiesConfiguration
{
    public class OrderDetailsEntityConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            #region Basic Configuration
            builder.ToTable("OrderDetails");
            builder.HasKey(od => od.Id);
            #endregion

            #region Relationships
            //OrderDetails -> MenuItem 
            builder.HasOne(od => od.MenuItem)
                .WithMany(m => m.OrderDetails)
                .HasForeignKey(od => od.MenuItemId);
            #endregion
        }
    }
}
