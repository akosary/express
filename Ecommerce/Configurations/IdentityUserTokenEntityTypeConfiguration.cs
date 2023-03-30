using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Configurations
{
    public class IdentityUserTokenEntityTypeConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
        {
            //Renaming to => UserTokens
            //Set Composite Pimary Key (UserId, LoginProvider, Name)
            builder
                .ToTable("UserTokens")
                .HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

            //Set Maximum Value Of Name
            builder
                .Property(ut => ut.Name)
                .HasMaxLength(128);

            //Set Maximum Value Of LoginProvider
            builder
                .Property(ut => ut.LoginProvider)
                .HasMaxLength(128);
        }
    }
}
