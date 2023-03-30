using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<Department> Departments { get; private set; }
        public IGenericRepository<Brand> Brands { get; private set; }
        public IGenericRepository<Supplier> Suppliers { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }
        public IGenericRepository<Product> Products { get; private set; }
        public IGenericRepository<UserProduct> UserProducts { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Departments = new GenericRepository<Department>(_context);
            Brands = new GenericRepository<Brand>(_context);
            Suppliers = new GenericRepository<Supplier>(_context);
            Categories = new GenericRepository<Category>(_context);
            Products = new GenericRepository<Product>(_context);
            UserProducts = new GenericRepository<UserProduct>(_context);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
