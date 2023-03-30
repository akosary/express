using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Configurations
{
    public class SupplierPhoneEntityTypeConfiguration : IEntityTypeConfiguration<SupplierPhone>
    {
        public void Configure(EntityTypeBuilder<SupplierPhone> builder)
        {
            //Set Composite Primary Key
            builder
                .HasKey(sp => new { sp.SupplierId, sp.PhoneNumber });
        }
    }
}
