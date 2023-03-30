using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public virtual List<UserProduct> UserProduct { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
