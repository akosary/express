using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Configurations
{
    public class IdentityRoleEntityTypeConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            //Renaming to => Roles
            builder
                .ToTable("Roles");

            //Set Maximum Value Of NormalizedName
            builder
                .Property(r => r.NormalizedName)
                .HasMaxLength(256);

            //Set Maximum Value Of Name
            builder
                .Property(r => r.Name)
                .HasMaxLength(256);
        }
    }
}
