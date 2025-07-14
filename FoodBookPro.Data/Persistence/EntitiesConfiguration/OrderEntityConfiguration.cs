using FoodBookPro.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodBookPro.Data.Persistence.EntitiesConfiguration
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            #region Basic Configuration
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);
            #endregion

            #region Relationships
            //Order -> OrderDetails
            builder.HasMany(o => o.Details)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            //Order -> Payment
            builder.HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId);
            #endregion
        }
    }
}
