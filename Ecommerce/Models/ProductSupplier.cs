namespace Ecommerce.Models
{
    public class ProductSupplier
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public int SupplierId { get; set; }
    }
}
