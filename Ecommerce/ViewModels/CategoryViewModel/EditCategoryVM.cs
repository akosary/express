using Ecommerce.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModels.CategoryViewModel
{
    public class EditCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string? ImgPath { get; set; }
        //public virtual List<Product> Products { get; set; }
        public virtual List<Department> Departments { get; set; }
        public virtual Department Department { get; set; }
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [DisplayName("Image")]
        public IFormFile? ImgFile { get; set; }
    }
}
