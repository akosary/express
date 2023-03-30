using Ecommerce.Configurations;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ecommerce.ViewModels.SupplierViewModels;
using Ecommerce.ViewModels.ProductViewModel;

namespace Ecommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductSupplier> ProductSuppliers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierPhone> SupplierPhones { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //User Configuration
            new ApplicationUserEntityTypeConfiguration().Configure(builder.Entity<ApplicationUser>());

            //Brand Configuration
            new BrandEntityTypeConfiguration().Configure(builder.Entity<Brand>());

            //Category Configuration
            new CategoryEntityTypeConfiguration().Configure(builder.Entity<Category>());

            //Department Configuration
            new DepartmentEntityTypeConfiguration().Configure(builder.Entity<Department>());

            //RoleClaim Configuration
            new IdentityRoleClaimEntityTypeConfiguration().Configure(builder.Entity<IdentityRoleClaim<string>>());

            //Role Configuration
            new IdentityRoleEntityTypeConfiguration().Configure(builder.Entity<IdentityRole>());

            //UserClaim Configuration
            new IdentityUserClaimsEntityTypeConfiguration().Configure(builder.Entity<IdentityUserClaim<string>>());

            //UserLogin Configuration
            new IdentityUserLoginEntityTypeConfiguration().Configure(builder.Entity<IdentityUserLogin<string>>());

            //UserRole Configuration
            new IdentityUserRoleEntityTypeConfiguration().Configure(builder.Entity<IdentityUserRole<string>>());

            //UserToken Configuration
            new IdentityUserTokenEntityTypeConfiguration().Configure(builder.Entity<IdentityUserToken<string>>());

            //Order Configuration
            new OrderEntityTypeConfiguration().Configure(builder.Entity<Order>());

            //OrderProduct Configuration
            new OrderProductEntityTypeConfiguration().Configure(builder.Entity<OrderProduct>());

            //Product Configuration
            new ProductEntityTypeConfiguration().Configure(builder.Entity<Product>());

            //ProductImage Configuration
            new ProductImageEntityTypeConfiguration().Configure(builder.Entity<ProductImage>());

            //Supplier Configuration
            new SupplierEntityTypeConfiguration().Configure(builder.Entity<Supplier>());

            //SupplierPhone Configuration
            new SupplierPhoneEntityTypeConfiguration().Configure(builder.Entity<SupplierPhone>());
        }
    }
}