using Ecommerce.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.ViewModels.ProductViewModel
{
    public class AddProductVM
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public virtual List<Category> Categories { get; set; }
        public virtual Category Category { get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public virtual List<Brand> Brands { get; set; }
        public virtual Brand Brand { get; set; }
        [DisplayName("Brand")]
        public int BrandId { get; set; }
        public virtual List<ProductSupplier>? ProductSuppliers { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        [DisplayName("Supplier")]
        public int SupplierId { get; set; }
        public int Stock { get; set; }
        public decimal UnitPrice { get; set; }
        public string? CoverImgPath { get; set; }
        [DisplayName("Cover Image")]
        public IFormFile ImgFile { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public List<string>? ImgPaths { get; set; }
        [DisplayName("Chooses Multiple Images")]
        public IFormFileCollection ImgFileMultiple { get; set; }


        //public virtual ICollection<ApplicationUser> User { get; set; }
        //public virtual List<UserProduct> UserProduct { get; set; }
        //public virtual ICollection<Order> Orders { get; set; }
        //public virtual List<OrderProduct> OrderProducts { get; set; }
    }
}
