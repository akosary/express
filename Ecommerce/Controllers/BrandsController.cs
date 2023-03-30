using AutoMapper;
using Ecommerce.Constants;
using Ecommerce.CustomValidations;
using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Ecommerce.ViewModels.BrandViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Ecommerce.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileUpload _fileUpload;
        private readonly IToastNotification _toastNotification;

        public BrandsController(IUnitOfWork unitOfWork, IMapper mapper, IFileUpload fileUpload, IToastNotification toastNotification)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileUpload = fileUpload;
            _toastNotification = toastNotification;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                ///var model = _mapper.Map<List<Brand>, List<IndexBrandViewModel>>(
                ///    await _unitOfWork.Brands.GetAllAsync()
                ///    );
                ///return View(model);
                return View(_mapper.Map<List<Brand>, List<IndexBrandVM>>(await _unitOfWork.Brands.GetAllAsync()));
            }
            catch (Exception)
            {

                return View();
            }
        }


        [Authorize(Permissions.Brands.Create)]
        public async Task<IActionResult> Create()
        {
            try
            {
                return View(new AddBrandVM());
            }
            catch (Exception)
            {
                return View();
            }
        }


        [Authorize(Permissions.Brands.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddBrandVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _toastNotification.AddErrorToastMessage(Alerts.ModelStateErrorMsg);
                    return View(new AddBrandVM());
                }


                ///Check If Image Has Valid Extension
                ///var extension = Path.GetExtension(model.ImgFile.FileName.TrimStart('.');
                ///if(Extension.IsImage(extension))
                if (!ExtensionValidation.IsImage(Path.GetExtension(model.ImgFile.FileName).TrimStart('.')))
                {
                    _toastNotification.AddErrorToastMessage(Alerts.ErrorMsgImgExtension);
                    return View(new AddBrandVM());
                }


                ///Check File Size Is Less Than 4MB
                ///fileSize = model.ImgFile.Length
                ///if (FileSize.IsValidSize(fileSize, 4))
                if (!FileSizeValidation.IsValidSize(model.ImgFile.Length, FileSize.ImgFileSize))
                {
                    _toastNotification.AddErrorToastMessage(Alerts.InavlidImgFileSize);
                    return View(new AddBrandVM());
                }


                //Upload Image To Server => Set Path Of The Image To ImgPath
                model.ImgPath = await _fileUpload
                    .UploadFileAsync(this.ControllerContext.RouteData.Values["Controller"].ToString()
                        , model.ImgFile);


                ///Map AddBrandViewModel Object To a Department Object
                ///Add Brand Object To Database & Save Changes
                ///var brand = _mapper.Map<Department>(model);
                ///await _unitOfWork.Brand.AddAsync(brand);
                ///await _unitOfWork.CommitAsync();
                await _unitOfWork.Brands.AddAsync(_mapper.Map<Brand>(model));
                await _unitOfWork.CommitAsync();

                //Add Sucess Notification
                _toastNotification.AddSuccessToastMessage(Alerts.AddedSucces);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }


        [Authorize(Permissions.Brands.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                ///var model = _mapper.Map<EditBrandViewModel>(
                ///await _unitOfWork.Brands.GetByIdAsync(id)
                ///);
                ///return View(model);
                return View(_mapper.Map<EditBrandVM>(await _unitOfWork.Brands.GetByIdAsync(id)));
            }
            catch (Exception)
            {
                return View();
            }
        }


        [Authorize(Permissions.Brands.Edit)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBrandVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _toastNotification.AddErrorToastMessage(Alerts.ModelStateErrorMsg);
                    return View();
                }


                if (model.ImgFile != null)
                {
                    if (!ExtensionValidation.IsImage(Path.GetExtension(model.ImgFile.FileName).TrimStart('.')))
                    {
                        _toastNotification.AddErrorToastMessage(Alerts.ErrorMsgImgExtension);
                        return View();
                    }

                    if (!FileSizeValidation.IsValidSize(model.ImgFile.Length, FileSize.ImgFileSize))
                    {
                        _toastNotification.AddErrorToastMessage(Alerts.InavlidImgFileSize);
                        return View();
                    }

                    //Remove Existing Image
                    System.IO.File.Delete($"wwwroot/{model.ImgPath}");

                    //Upload Image To Server =>  Set Path Of The Image To ImgPath
                    model.ImgPath = await _fileUpload.
                        UploadFileAsync(this.ControllerContext.RouteData.Values["Controller"].ToString(), model.ImgFile);
                }


                _unitOfWork.Brands.Update(_mapper.Map<Brand>(model));
                await _unitOfWork.CommitAsync();


                _toastNotification.AddSuccessToastMessage(Alerts.UpdateSucces);


                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }


        [Authorize(Permissions.Brands.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return View(_mapper.Map<DeleteBrandVM>(await _unitOfWork.Brands.GetByIdAsync(id)));
            }
            catch (Exception)
            {
                return View();
            }
        }


        [Authorize(Permissions.Brands.Delete)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteBrandVM model)
        {
            try
            {
                //Remove Existing Image
                System.IO.File.Delete($"wwwroot/{model.ImgPath}");

                _unitOfWork.Brands.Delete(_mapper.Map<Brand>(model));
                await _unitOfWork.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
