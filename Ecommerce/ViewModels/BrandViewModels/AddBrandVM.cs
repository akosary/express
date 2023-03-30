using System.ComponentModel;

namespace Ecommerce.ViewModels.BrandViewModels
{
    public class AddBrandVM
    {
        public string Name { get; set; }
        public string? ImgPath { get; set; }
        [DisplayName("Image")]
        public IFormFile ImgFile { get; set; }
    }
}
