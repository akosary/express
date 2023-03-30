using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Configurations
{
    public class IdentityUserLoginEntityTypeConfiguration : IEntityTypeConfiguration<IdentityUserLogin<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
        {
            //Renaming to => UserLogins
            //Set Composite Pimary Key (LoginProvider, ProviderKey)
            builder
                .ToTable("UserLogins")
                .HasKey(ul => new {ul.LoginProvider, ul.ProviderKey});

            //Set Maximum Value Of UserId
            builder
                .Property(ul => ul.UserId)
                .HasMaxLength(450);

            //Set Maximum Value Of ProviderKey
            builder
                .Property(ul => ul.ProviderKey)
                .HasMaxLength(128);

            //Set Maximum Value Of LoginProvider
            builder
                .Property(ul => ul.LoginProvider)
                .HasMaxLength(128);
        }
    }
}
