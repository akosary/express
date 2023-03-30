using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Configurations
{
    public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            //Renamin to => Users
            builder
                .ToTable("Users");

            //set Maximum Length of First Name
            builder
                .Property(au => au.FirstName)
                .HasMaxLength(50);

            //set Maximum Length of Last Name
            builder
                .Property(au => au.LastName)
                .HasMaxLength(50);

            //Set Maximum Value Of UserName
            builder
                .Property(au => au.UserName)
                .HasMaxLength(256);

            //Set Maximum Value Of NormalizedUserName
            builder
                .Property(au => au.NormalizedUserName)
                .HasMaxLength(256);

            //Set Maximum Value Of NormalizedEmail
            builder
                .Property(au => au.NormalizedEmail)
                .HasMaxLength(256);

            //Set Maximum Value Of Email
            builder
                .Property(au => au.Email)
                .HasMaxLength(256);




            //Configure M To N Rleationship Between User & Product Tables
            builder
                .HasMany(u => u.Product)
                .WithMany(p => p.User)
                .UsingEntity<UserProduct>(
                    t => t
                        .HasOne(up => up.Product)
                        .WithMany(p => p.UserProduct)
                        .HasForeignKey(up => up.ProductId),
                    t => t
                        .HasOne(up => up.User)
                        .WithMany(u => u.UserProduct)
                        .HasForeignKey(up => up.UserId),
                    t => t
                        .HasKey(up => new { up.UserId, up.ProductId })
                );
        }
    }
}
