namespace Ecommerce.Models
{
    public class OrderProduct
    {
        //[ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        //[ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        //[Display(Name ="Quantity")]
        //[Required]
        public int Quantity { get; set; }
        //[Required]
        public decimal Amount { get; set; }

    }
}
