using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Configurations
{
    public class BrandEntityTypeConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            //Set Primary Key
            builder
                .HasKey(b => b.Id);

            //Set Maximum Length Of Name
            builder
                .Property(b => b.Name)
                .HasMaxLength(100);

            //Set Maximum Length Of ImgPath
            builder
                .Property(b => b.ImgPath)
                .HasMaxLength(256);
        }
    }
}
