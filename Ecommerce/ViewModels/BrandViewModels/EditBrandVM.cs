using System.ComponentModel;

namespace Ecommerce.ViewModels.BrandViewModels
{
    public class EditBrandVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImgPath { get; set; }
        [DisplayName("Image")]
        public IFormFile? ImgFile { get; set; }
    }
}
