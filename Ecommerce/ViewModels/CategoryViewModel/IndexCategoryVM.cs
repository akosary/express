using Ecommerce.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModels.CategoryViewModel
{
    public class IndexCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //[DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.ImageUrl)]
        [DisplayName("Image")]
        public string ImgPath { get; set; }
        //public virtual List<Product> Products { get; set; }
        public virtual Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
