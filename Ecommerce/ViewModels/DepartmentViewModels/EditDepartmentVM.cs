using System.ComponentModel;

namespace Ecommerce.ViewModels.DepartmentViewModels
{
    public class EditDepartmentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImgPath { get; set; }
        [DisplayName("Image")]
        public IFormFile? ImgFile { get; set; }
    }
}
