using Ecommerce.Models;
using System.ComponentModel;

namespace Ecommerce.ViewModels.ProductViewModel
{
    public class ListFilterProductVM
    {
        public List<IndexProductVM> IndexProductVMs { get; set; }
        public List<Department> Departments { get; set; }
        [DisplayName("Departments")]
        public int DepartmentId { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        [DisplayName("Category")]
        public int CategoryId { get; set; }
    }
}
