using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Configurations
{
    public class IdentityUserRoleEntityTypeConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            //Renaming to => UserRoles
            //Set Composite Pimary Key (UserId, RoleId)
            builder
                .ToTable("UserRoles")
                .HasKey(ur => new { ur.UserId, ur.RoleId });
        }
    }
}
