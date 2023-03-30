namespace Ecommerce.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public virtual List<Category> Categories { get; set; }
    }
}
