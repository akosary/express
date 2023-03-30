namespace Ecommerce.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual List<ProductSupplier> ProductSuppliers { get; set; }
        public virtual List<SupplierPhone> SupplierPhones { get; set; }
    }
}