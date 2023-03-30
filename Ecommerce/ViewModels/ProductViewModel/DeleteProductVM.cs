using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModels.ProductViewModel
{
    public class DeleteProductVM
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Category> Categories { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Brand> Brands { get; set; }
        public virtual Brand Brand { get; set; }
        public int Stock { get; set; }
        public decimal UnitPrice { get; set; }
        public string? CoverImgPath { get; set; }
        public virtual List<ProductSupplier> ProductSuppliers { get; set; }
        public virtual List<ProductImage> ProductImages { get; set; }
    }
}
