namespace Ecommerce.Models
{
    public class ProductImage
    {
        public Product Product { get; set; }
		public int ProductId { get; set; }
		public string ImgPath { get; set; }
    }
}
