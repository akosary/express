namespace Ecommerce.Models
{
    public class Order
    {
        //[Key]
        public int Id { get; set; }
        //[DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }

        //[MaxLength(100)]
        public string Status { get; set; } //= "Order_Pending";

        //[MaxLength(150)]
        public string PayStatus { get; set; } //= "Pending";
        //[DataType(DataType.Date)]
        public DateTime? PayDate { get; set; }
        //[DataType(DataType.MultilineText)]
        //[Required(ErrorMessage = "Enter Your Address")]
        public string ShipAddress { get; set; }
        public string ShipStatus { get; set; } //= "Not_Arrived";
        public string ShipTrackNo { get; set; } //= Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual List<OrderProduct> OrderProducts { get; set; }
    }
}
