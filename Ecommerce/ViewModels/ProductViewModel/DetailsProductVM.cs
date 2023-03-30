using Ecommerce.Models;

namespace Ecommerce.ViewModels.ProductViewModel
{
    public class DetailsProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string CoverImgPath { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual List<ProductSupplier> ProductSuppliers { get; set; }
        public virtual List<Supplier> Suppliers { get; set; }
        public virtual List<ProductImage> ProductImages { get; set; }
        public virtual List<UserProduct> UserProduct { get; set; }
    }
}
