using Ecommerce.Models;
namespace Ecommerce.ViewModels.ProductViewModel
{
    public class IndexProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string CoverImgPath { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public virtual Brand Brand { get; set; }
        public int BrandId { get; set; }




        //public virtual List<ProductImage> ProductImages { get; set; }
        //public virtual ICollection<Supplier> Suppliers { get; set; }
        //public virtual List<ProductSupplier> ProductSuppliers { get; set; }
        //public virtual ICollection<ApplicationUser> User { get; set; }
        //public virtual List<UserProduct> UserProduct { get; set; }
        //public virtual ICollection<Order> Orders { get; set; }
        //public virtual List<OrderProduct> OrderProducts { get; set; }
    }
}
