using AutoMapper;
using Ecommerce.Constants;
using Ecommerce.CustomValidations;
using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Ecommerce.ViewModels.ProductViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Ecommerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileUpload _fileUpload;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper,
                IFileUpload fileUpload, IToastNotification toastNotification,
                UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileUpload = fileUpload;
            _toastNotification = toastNotification;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int? deptId, int? catId)
        {
            try
            {
                var viewModel = new ListFilterProductVM();
                viewModel.IndexProductVMs = _mapper.Map<List<Product>, List<IndexProductVM>>(
                        await _unitOfWork.Products
                            .FindAllAsync(p => p.Id > 0, new string[] { "Brand", "Category" })
                );
                viewModel.Departments = _mapper.Map<List<Department>>(await _unitOfWork.Departments.GetAllAsync());
                return View(viewModel);
            }
            catch (Exception)
            {
                return View();
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string search)
        {
            try
            {
                var viewModel = new ListFilterProductVM();
                viewModel.IndexProductVMs = _mapper.Map<List<Product>, List<IndexProductVM>>(
                        await _unitOfWork.Products
                            .FindAllAsync(p => p.Name.ToLower().Contains(search.ToLower()),
                            new string[] { "Brand", "Category" })
                );
                viewModel.Departments = _mapper.Map<List<Department>>(await _unitOfWork.Departments.GetAllAsync());
                return View(viewModel);
            }
            catch (Exception)
            {
                //var viewModel = _mapper.Map<List<Product>, List<IndexProductVM>>(
                //        await _unitOfWork.Products.FindAllAsync(p => p.Id != null,
                //        new string[] { "Brand", "Category" })
                //);
                return View(/*viewModel*/);
            }
        }


        [AllowAnonymous]
        public async Task<IActionResult> GetCategories(int deptId)
        {
            return Ok(await _unitOfWork.Categories.FindAllAsync(c =>
                    c.DepartmentId == deptId)
                );
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetProductCategory(int categoryId, string? search = null)
        {
            try
            {
                if (search == null)
                {
                    return View(new ListFilterProductVM
                    {
                        IndexProductVMs = _mapper.Map<List<Product>, List<IndexProductVM>>(
                        await _unitOfWork.Products.FindAllAsync(p => p.CategoryId == categoryId,
                        new string[] { "Brand", "Category" })),
                        Departments = _mapper.Map<List<Department>>(await _unitOfWork.Departments.GetAllAsync())
                    });
                }

                return View(new ListFilterProductVM
                {
                    IndexProductVMs = _mapper.Map<List<Product>, List<IndexProductVM>>(
                        await _unitOfWork.Products.FindAllAsync(p => p.CategoryId == categoryId && p.Name.ToLower().Contains(search.ToLower()),
                        new string[] { "Brand", "Category" })),
                    Departments = _mapper.Map<List<Department>>(await _unitOfWork.Departments.GetAllAsync())
                });
            }
            catch (Exception)
            {
                return View();
            }
        }


        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var viewModel = _mapper.Map<DetailsProductVM>(
                    await _unitOfWork.Products.FindAsync(p => p.Id == id,
                    new string[] { "ProductSuppliers", "ProductImages", "Suppliers", "UserProduct" }));
            return View(viewModel);

        }


        [Authorize(Permissions.Products.View)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(DetailsProductVM model)
        {
            var viewModel = _mapper.Map<DetailsProductVM>(
                    await _unitOfWork.Products.FindAsync(p => p.Id == model.Id,
                    new string[] { "ProductSuppliers", "ProductImages", "Suppliers", "UserProduct" }));
            return View(viewModel);

        }


        [Authorize(Permissions.Products.Create)]
        public async Task<IActionResult> Create()
        {
            try
            {
                ///var model = new AddProductVM
                ///{
                ///    Categories = await _unitOfWork.Categories.GetAllAsync(),
                ///    Brands = await _unitOfWork.Brands.GetAllAsync(),
                ///    Suppliers = await _unitOfWork.Suppliers.GetAllAsync()
                ///};
                ///return View(model);
                return View(new AddProductVM
                {
                    Categories = await _unitOfWork.Categories.GetAllAsync(),
                    Brands = await _unitOfWork.Brands.GetAllAsync(),
                    Suppliers = await _unitOfWork.Suppliers.GetAllAsync()
                });
            }
            catch (Exception)
            {
                return View();
            }
        }


        [Authorize(Permissions.Products.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddProductVM model)
        {
            try
            {
                //Re-render The View When ModelState Validation Failed.
                if (!ModelState.IsValid)
                {
                    _toastNotification.AddErrorToastMessage(Alerts.ModelStateErrorMsg);
                    return View(new AddProductVM());
                }

                if (model.ImgFile != null)
                {
                    ///Check If Image Has Valid Extension
                    ///var extension = Path.GetExtension(model.ImgFile.FileName.TrimStart('.');
                    ///if(Extension.IsImage(extension))
                    if (!ExtensionValidation.IsImage(Path.GetExtension(model.ImgFile.FileName).TrimStart('.')))
                    {
                        _toastNotification.AddErrorToastMessage(Alerts.ErrorMsgImgExtension);
                        return View(new AddProductVM());
                    }

                    ///Check File Size Is Less Than 4MB
                    ///fileSize = model.ImgFile.Length
                    ///if (FileSize.IsValidSize(fileSize, 4))
                    if (!FileSizeValidation.IsValidSize(model.ImgFile.Length, FileSize.ImgFileSize))
                    {
                        _toastNotification.AddErrorToastMessage(Alerts.InavlidImgFileSize);
                        return View(new AddProductVM());
                    }

                    //Set Path Of The Image To CoverImgPath
                    model.CoverImgPath = await _fileUpload
                        .UploadFileAsync(ControllersNames.ProductController, model.ImgFile);

                }

                if (model.ImgFileMultiple != null)
                {
                    ///Check If Image Has Valid Extension
                    ///var extension = Path.GetExtension(model.ImgFile.FileName.TrimStart('.');
                    ///if(Extension.IsImage(extension))
                    foreach (var img in model.ImgFileMultiple)
                        if (!ExtensionValidation.IsImage(Path.GetExtension(img.FileName).TrimStart('.')))
                        {
                            _toastNotification.AddErrorToastMessage(Alerts.ErrorMsgImgExtension);
                            return View(new AddProductVM());
                        }

                    ///Check File Size Is Less Than 4MB
                    ///fileSize = model.ImgFile.Length
                    ///if (FileSize.IsValidSize(fileSize, 4))
                    foreach (var img in model.ImgFileMultiple)
                        if (!FileSizeValidation.IsValidSize(img.Length, FileSize.ImgFileSize))
                        {
                            _toastNotification.AddErrorToastMessage(Alerts.InavlidImgFileSize);
                            return View(new AddProductVM());
                        }

                    //Set Paths Of The Images To ProductImage Table
                    model.ProductImages = await _fileUpload.UploadProductImgsAsync(
                                ControllersNames.ProductController, model.ImgFileMultiple);
                }

                var psLst = new List<ProductSupplier>();
                psLst.Add(new ProductSupplier { SupplierId = model.SupplierId });
                model.ProductSuppliers = psLst;

                ///Map AddProductVM Object To a Product Object
                ///Add Product Object To Database & Save Changes
                ///var product = _mapper.Map<product>(model);
                ///await _unitOfWork.Products.AddAsync(product);
                ///await _unitOfWork.CommitAsync();
                await _unitOfWork.Products.AddAsync(_mapper.Map<Product>(model));

                await _unitOfWork.CommitAsync();

                //Add Sucess Notification
                _toastNotification.AddSuccessToastMessage(Alerts.AddedSucces);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                //Add Sucess Notification
                _toastNotification.AddErrorToastMessage(Alerts.ModelStateErrorMsg);
                ///var model = new AddProductVM
                ///{
                ///    Categories = await _unitOfWork.Categories.GetAllAsync(),
                ///    Brands = await _unitOfWork.Brands.GetAllAsync(),
                ///    Suppliers = await _unitOfWork.Suppliers.GetAllAsync()
                ///};
                ///return View(model);
                return View(new AddProductVM
                {
                    Categories = await _unitOfWork.Categories.GetAllAsync(),
                    Brands = await _unitOfWork.Brands.GetAllAsync(),
                    Suppliers = await _unitOfWork.Suppliers.GetAllAsync()
                });
            }

        }


        [Authorize(Permissions.Products.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = _mapper.Map<EditProductVM>(await _unitOfWork.Products.FindAsync(
                        p => p.Id == id, new string[] { "Suppliers", "ProductImages" })
                        );
                model.Brands = await _unitOfWork.Brands.GetAllAsync();
                model.Categories = await _unitOfWork.Categories.GetAllAsync();
                //model.Suppliers = await _unitOfWork.Suppliers.GetAllAsync();
                //model.ProductImages = await _unitOfWork.ProductImages.FindAllAsync(pi => pi.ProductId == id);
                return View(model);
            }
            catch (Exception)
            {
                return View();
            }
        }


        [Authorize(Permissions.Products.Edit)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductVM model)
        {
            try
            {
                //Re-render The View When ModelState Validation Failed.
                if (!ModelState.IsValid)
                {
                    _toastNotification.AddErrorToastMessage(Alerts.ModelStateErrorMsg);
                    return View(new EditProductVM());
                }

                if (model.ImgFile != null)
                {
                    ///Check If Image Has Valid Extension
                    ///var extension = Path.GetExtension(model.ImgFile.FileName.TrimStart('.');
                    ///if(Extension.IsImage(extension))
                    if (!ExtensionValidation.IsImage(Path.GetExtension(model.ImgFile.FileName).TrimStart('.')))
                    {
                        _toastNotification.AddErrorToastMessage(Alerts.ErrorMsgImgExtension);
                        return View(new AddProductVM());
                    }

                    ///Check File Size Is Less Than 4MB
                    ///fileSize = model.ImgFile.Length
                    ///if (FileSize.IsValidSize(fileSize, 4))
                    if (!FileSizeValidation.IsValidSize(model.ImgFile.Length, FileSize.ImgFileSize))
                    {
                        _toastNotification.AddErrorToastMessage(Alerts.InavlidImgFileSize);
                        return View(new AddProductVM());
                    }

                    //Remove Existing Image
                    System.IO.File.Delete($"wwwroot/{model.CoverImgPath}");

                    //Set Path Of The Image To CoverImgPath
                    model.CoverImgPath = await _fileUpload
                        .UploadFileAsync(ControllersNames.ProductController, model.ImgFile);

                }

                if (model.ImgFileMultiple != null)
                {
                    ///Check If Image Has Valid Extension
                    ///var extension = Path.GetExtension(model.ImgFile.FileName.TrimStart('.');
                    ///if(Extension.IsImage(extension))
                    foreach (var img in model.ImgFileMultiple)
                        if (!ExtensionValidation.IsImage(Path.GetExtension(img.FileName).TrimStart('.')))
                        {
                            _toastNotification.AddErrorToastMessage(Alerts.ErrorMsgImgExtension);
                            return View(new EditProductVM());
                        }

                    //Check File Size Is Less Than 4MB
                    foreach (var img in model.ImgFileMultiple)
                        ///fileSize = model.ImgFile.Length
                        ///if (FileSize.IsValidSize(fileSize, 4))
                        if (!FileSizeValidation.IsValidSize(img.Length, FileSize.ImgFileSize))
                        {
                            _toastNotification.AddErrorToastMessage(Alerts.InavlidImgFileSize);
                            return View(new EditProductVM());
                        }

                    //Remove Existing Images
                    foreach (var img in model.ProductImages)
                        System.IO.File.Delete($"wwwroot/{img.ImgPath}");

                    //Set Paths Of The Images To ProductImage Table
                    model.ProductImages = await _fileUpload.UploadProductImgsAsync(
                                ControllersNames.ProductController, model.ImgFileMultiple);
                }

                ///Map EidtProductVM Object To a Product Object
                ///Update Product Object To Database & Save Changes
                ///var product = _mapper.Map<product>(model);
                ///await _unitOfWork.Products.AddAsync(product);
                ///await _unitOfWork.CommitAsync();
                _unitOfWork.Products.Update(_mapper.Map<Product>(model));
                await _unitOfWork.CommitAsync();

                //Add Sucess Notification
                _toastNotification.AddSuccessToastMessage(Alerts.UpdateSucces);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                var viewModel = _mapper.Map<EditProductVM>(await _unitOfWork.Products.FindAsync(
                p => p.Id == model.Id, new string[] { "ProductImages", "Suppliers" })
                );
                model.Brands = await _unitOfWork.Brands.GetAllAsync();
                model.Categories = await _unitOfWork.Categories.GetAllAsync();
                //model.Suppliers = await _unitOfWork.Suppliers.GetAllAsync();
                //model.ProductImages = await _unitOfWork.ProductImages
                //    .FindAllAsync(pi => pi.ProductId == model.Id, new string[] { "Product" });
                return View(viewModel);
            }

        }

        [Authorize(Permissions.Products.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                ///var viewModel = _mapper.Map<DeleteProductVM>(await _unitOfWork.Products.GetByIdAsync(id));
                ///viewModel.ProductImages = await _unitOfWork.ProductImages.FindAllAsync(
                ///    pi => pi.ProductId == id);
                ///viewModel.ProductSuppliers = await _unitOfWork.ProductSuppliers.FindAllAsync(
                ///    ps => ps.ProductId == id);
                var viewModel = _mapper.Map<DeleteProductVM>(await _unitOfWork.Products
                        .FindAsync(p => p.Id == id, new string[] { "ProductImages", "ProductSuppliers" })
                        );
                return View(viewModel);
            }
            catch (Exception)
            {
                return View();
            }
        }


        [Authorize(Permissions.Products.Delete)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteProductVM model)
        {
            try
            {
                //Remove Existing Cover Image
                System.IO.File.Delete($"wwwroot/{model.CoverImgPath}");

                foreach (var img in model.ProductImages)
                    System.IO.File.Delete($"wwwroot/{img.ImgPath}");

                _unitOfWork.Products.Delete(_mapper.Map<Product>(model));
                await _unitOfWork.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                //var viewModel = _mapper.Map<DeleteProductVM>(await _unitOfWork.Products.GetByIdAsync(model.Id));
                //viewModel.ProductImages = await _unitOfWork.ProductImages.FindAllAsync(
                //    pi => pi.ProductId == model.Id);
                //viewModel.ProductSuppliers = await _unitOfWork.ProductSuppliers.FindAllAsync(
                //    ps => ps.ProductId == model.Id);

                var viewModel = _mapper.Map<DeleteProductVM>(await _unitOfWork.Products
                        .FindAsync(p => p.Id == model.Id, new string[] { "ProductImages", "ProductSuppliers" })
                        );
                return View(viewModel);
            }
        }

        [Authorize(Permissions.Products.View)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRemoveFavourite(DetailsProductVM model)
        {
            if(!ModelState.IsValid)
                return View();
            var favLst = await _unitOfWork.Products.FindAllAsync(
                p => p.Id == model.Id && p.User.Where(p => p.Id == _userManager.GetUserAsync(User).Result.Id) != null,
                new string[] { "UserProduct" }
                );

            return Json("success");
        }
    }
}
