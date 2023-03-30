using System.ComponentModel;

namespace Ecommerce.ViewModels.BrandViewModels
{
    public class DeleteBrandVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Image")]
        public string ImgPath { get; set; }
    }
}
