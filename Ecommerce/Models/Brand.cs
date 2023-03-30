using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
