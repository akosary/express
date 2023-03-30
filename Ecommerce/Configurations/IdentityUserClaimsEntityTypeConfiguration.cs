using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Configurations
{
    public class IdentityUserClaimsEntityTypeConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
        {
            //Renaming to => UserClaims
            builder
                .ToTable("UserClaims");

            //Set Maximum Value Of UserId
            builder
                .Property(uc => uc.UserId)
                .HasMaxLength(450);
        }
    }
}
