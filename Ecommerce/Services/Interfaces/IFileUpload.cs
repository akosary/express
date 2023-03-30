using Ecommerce.Models;

namespace Ecommerce.Services.Interfaces
{
    public interface IFileUpload 
    {
        Task<string> UploadFileAsync(string controller, IFormFile iFormFile);
        Task<List<ProductImage>> UploadProductImgsAsync(string controller, IFormFileCollection iFormFileCollection);
    }
}
