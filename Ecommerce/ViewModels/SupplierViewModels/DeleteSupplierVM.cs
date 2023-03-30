using Ecommerce.Models;

namespace Ecommerce.ViewModels.SupplierViewModels
{
    public class DeleteSupplierVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public virtual List<SupplierPhone> SupplierPhones { get; set; }
    }
}
