using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Configurations
{
    public class SupplierEntityTypeConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            //Set Primary Key
            builder
                .HasKey(s => s.Id);

            //Set Maximum Length Of Name
            builder
                .Property(s => s.Name)
                .HasMaxLength(100);

            //Set Maximum Length Of Zip Code
            builder
                .Property(s => s.Zip)
                .HasMaxLength(10);

            //Set Maximum Length Of Street
            builder
                .Property(s => s.Street)
                .HasMaxLength(100);

            //Set Maximum Length Of City
            builder
                .Property(s => s.City)
                .HasMaxLength(100);

            //Configure Rlationship Between Suppliers & SupplierPhones Tables
            builder
                .HasMany(s => s.SupplierPhones)
                .WithOne(sp => sp.Supplier)
                .HasForeignKey(s => s.SupplierId);
        }
    }
}
