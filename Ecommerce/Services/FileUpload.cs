using Ecommerce.Models;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUpload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFileAsync(string controller, IFormFile iFormFile)
        {
            if (iFormFile != null)
            {
                var imgPath = $"Images/{controller}/{Guid.NewGuid()}__{iFormFile.FileName}";
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, imgPath);
                await iFormFile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                return imgPath;
            }

            return string.Empty;
        }

        public async Task<List<ProductImage>> UploadProductImgsAsync(string controller, IFormFileCollection iFormFileCollection)
        {
            List<ProductImage> ProImgs = new List<ProductImage>();
            foreach (var img in iFormFileCollection)
            {
                ///var ImagePath = $"img/{controller}/" + Guid.NewGuid() + "_" + img.FileName;
                ///string ServerFolder = Path.Combine(_webHostEnvironment.WebRootPath, ImagePath);
                ///await img.CopyToAsync(new FileStream(ServerFolder, FileMode.Create));
                ///
                ///ProductImage pi = new ProductImage
                ///{
                ///    ImgPath = ImagePath
                ///};
                ///
                ///ProImgs.Add(pi);

                var ImagePath = await UploadFileAsync(controller, img);

                ProImgs.Add(new ProductImage { ImgPath = ImagePath });
            }
            return ProImgs;
        }

    }
}
