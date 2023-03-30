using Ecommerce.Models;
using System.ComponentModel;

namespace Ecommerce.ViewModels.CategoryViewModel
{
    public class DeleteCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImgPath { get; set; }
        public virtual Department Department { get; set; }
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
    }
}
