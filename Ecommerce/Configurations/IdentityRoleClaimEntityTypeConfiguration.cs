using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Configurations
{
    public class IdentityRoleClaimEntityTypeConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
        {
            //Renaming to => RoleClaims
            builder
                .ToTable("RoleClaims");

            //Set Maximum Value Of RoleId
            builder
                .Property(r => r.RoleId)
                .HasMaxLength(450);
        }
    }
}
