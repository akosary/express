using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Configurations
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //Set Primary Key
            builder
                .HasKey(c => c.Id);

            //Set Maximum Length Of Name
            builder
                .Property(c => c.Name)
                .HasMaxLength(100);

            //Set Maximum Length Of ImgPath
            builder
                .Property(c => c.ImgPath)
                .HasMaxLength(256);

            //Configure Relationship Between Categories & Departments Tables
            builder
                .HasOne(c => c.Department)
                .WithMany(d => d.Categories)
                .HasForeignKey(c => c.DepartmentId);
                
        }
    }
}
