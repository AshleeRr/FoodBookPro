using FoodBookPro.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodBookPro.Data.Persistence.EntitiesConfiguration
{
    public class PaymentEntityConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            #region Basic Configuration
            builder.ToTable("Payments");
            builder.HasKey(p => p.Id);
            #endregion
        }
    }
}
