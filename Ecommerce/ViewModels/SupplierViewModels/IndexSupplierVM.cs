using Ecommerce.Models;

namespace Ecommerce.ViewModels.SupplierViewModels
{
    public class IndexSupplierVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Zip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public List<SupplierPhone> SupplierPhones { get; set; }
    }
}
