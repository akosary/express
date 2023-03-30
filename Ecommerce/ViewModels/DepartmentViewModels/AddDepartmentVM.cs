using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.ViewModels.DepartmentViewModels
{
    public class AddDepartmentVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImgPath { get; set; }
        [DisplayName("Image")]
        public IFormFile ImgFile { get; set; }
    }
}
