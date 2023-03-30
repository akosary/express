using Ecommerce.Data;
using Ecommerce.Models;

namespace Ecommerce.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Department> Departments { get; }
        IGenericRepository<Brand> Brands { get; }
        IGenericRepository<Supplier> Suppliers { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Product> Products { get; }
        IGenericRepository<UserProduct> UserProducts { get; }

        public Task<int> CommitAsync();
    }
}
