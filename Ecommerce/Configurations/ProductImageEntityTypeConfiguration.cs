using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Configurations
{
    public class ProductImageEntityTypeConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            //Set Composite Primary Key
            builder
                .HasKey(pi => new { pi.ProductId, pi.ImgPath });

            //Set Maximum Length Of ImgPath
            builder
                .Property(pi => pi.ImgPath)
                .HasMaxLength(256);

            //Configure Relationship Between ProductImages & Product Tables
            builder
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId);
        }
    }
}
