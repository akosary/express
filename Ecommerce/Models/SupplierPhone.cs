namespace Ecommerce.Models
{
    public class SupplierPhone
    {
        public int SupplierId { get; set; }
        //[RegularExpression(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{5}$", ErrorMessage = "Numbers are not allowed !")]
        //[Required(ErrorMessage = "You have enter a phone number")]
        public string PhoneNumber { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
