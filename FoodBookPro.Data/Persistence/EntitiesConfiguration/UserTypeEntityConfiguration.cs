using FoodBookPro.Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodBookPro.Data.Persistence.EntitiesConfiguration
{
    public class UserTypeEntityConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            #region Basic Configuration
            builder.ToTable("UserTypes");
            builder.HasKey(ut => ut.Id);
            #endregion

            #region Relationships
            // UserType -> Users
            builder.HasMany(ut => ut.Users)
                .WithOne(u => u.UserType)
                .HasForeignKey(u => u.Role);
            #endregion
        }
    }
}