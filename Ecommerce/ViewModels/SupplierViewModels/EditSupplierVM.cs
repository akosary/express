using Ecommerce.Models;
using System.ComponentModel;

namespace Ecommerce.ViewModels.SupplierViewModels
{
    public class EditSupplierVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        [DisplayName("Phone")]
        public List<SupplierPhone> SupplierPhones { get; set; }
    }
}
