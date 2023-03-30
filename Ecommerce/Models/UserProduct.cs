namespace Ecommerce.Models
{
    public class UserProduct
    {
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }

    }
}
