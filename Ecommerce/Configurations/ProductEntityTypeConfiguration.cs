using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Configurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //Set Primary Key
            builder
                .HasKey(p => p.Id);

            //Set Maximum Length Of Name
            builder
                .Property(p => p.Name)
                .HasMaxLength(256);

            //Set Maximum Length Of CoverImgPath
            builder
                .Property(p => p.CoverImgPath)
                .HasMaxLength(256);

            //Configure Relationship Between Products & Categories Tables
            builder
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CategoryId);

            //Configure Relationship Between Products & Brands Tables
            builder
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(c => c.BrandId);

            //Configure M To N Rleationship Between Products & Suppliers Tables
            builder
                .HasMany(p => p.Suppliers)
                .WithMany(s => s.Products)
                .UsingEntity<ProductSupplier>(
                    t => t
                        .HasOne(ps => ps.Supplier)
                        .WithMany(s => s.ProductSuppliers)
                        .HasForeignKey(ps => ps.SupplierId),
                    t => t
                        .HasOne(ps => ps.Product)
                        .WithMany(p => p.ProductSuppliers)
                        .HasForeignKey(ps => ps.ProductId),
                    t => t
                        .HasKey(ps => new { ps.ProductId, ps.SupplierId })
                );

            //Configure M To N Rleationship Between Products & Orders Tables
            builder
                .HasMany(p => p.Orders)
                .WithMany(o => o.Products)
                .UsingEntity<OrderProduct>(
                    t => t
                        .HasOne(op => op.Order)
                        .WithMany(o => o.OrderProducts)
                        .HasForeignKey(op => op.OrderId),
                    t => t
                        .HasOne(op => op.Product)
                        .WithMany(p => p.OrderProducts)
                        .HasForeignKey(op => op.ProductId),
                    t => t
                    .HasKey(op => new { op.OrderId, op.ProductId })
                );
        }
    }
}
