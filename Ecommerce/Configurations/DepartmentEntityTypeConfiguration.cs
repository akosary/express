using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Configurations
{
    public class DepartmentEntityTypeConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //Set Primary Key
            builder
                .HasKey(d => d.Id);

            //Set Maximum Length Of Name
            builder
                .Property(d => d.Name)
                .HasMaxLength(100);

            //Set Maximum Length Of ImgPath
            builder
                .Property(d => d.ImgPath)
                .HasMaxLength(256);
        }
    }
}
