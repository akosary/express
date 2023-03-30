using AutoMapper;
using Ecommerce.Constants;
using Ecommerce.CustomValidations;
using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Ecommerce.ViewModels.CategoryViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Ecommerce.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileUpload _fileUpload;
        private readonly IToastNotification _toastNotification;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper, IFileUpload fileUpload, IToastNotification toastNotification)
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
                ///var model = _mapper.Map<List<Category>, List<IndexCategoryVM>>(
                ///       await _unitOfWork.Categories.GetAllAsync());
                ///return View(model);
                return View(_mapper.Map<List<Category>, List<IndexCategoryVM>>(await _unitOfWork.Categories.GetAllAsync()));
            }
            catch (Exception)
            {
                return View();
            }
        }


        [Authorize(Permissions.Categories.Create)]
        public async Task<IActionResult> Create()
        {
            try
            {
                return View(new AddCategoryVM { Departments = await _unitOfWork.Departments.GetAllAsync()});
            }
            catch (Exception)
            {
                return View();
            }
        }

        [Authorize(Permissions.Categories.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddCategoryVM model)
        {
            try
            {
                //Re-render The View When ModelState Validation Failed.
                if (!ModelState.IsValid)
                {
                    _toastNotification.AddErrorToastMessage(Alerts.ModelStateErrorMsg);
                    return View(new AddCategoryVM());
                }

                ///Check If Image Has Valid Extension
                ///var extension = Path.GetExtension(model.ImgFile.FileName.TrimStart('.');
                ///if(Extension.IsImage(extension))
                if (!ExtensionValidation.IsImage(Path.GetExtension(model.ImgFile.FileName).TrimStart('.')))
                {
                    _toastNotification.AddErrorToastMessage(Alerts.ErrorMsgImgExtension);
                    return View(new AddCategoryVM());
                }

                ///Check File Size Is Less Than 4MB
                ///fileSize = model.ImgFile.Length
                ///if (FileSize.IsValidSize(fileSize, 4))
                if (!FileSizeValidation.IsValidSize(model.ImgFile.Length, FileSize.ImgFileSize))
                {
                    _toastNotification.AddErrorToastMessage(Alerts.InavlidImgFileSize);
                    return View(new AddCategoryVM());
                }

                //Set Path Of The Image To ImgPath Property
                model.ImgPath = await _fileUpload
                    .UploadFileAsync(this.ControllerContext.RouteData.Values["Controller"].ToString()
                        , model.ImgFile);

                ///Map AddCategoryVM Object To a Category Object
                ///Add Category Object To Database & Save Changes
                ///var category = _mapper.Map<Category>(model);
                ///await _unitOfWork.Categories.AddAsync(category);
                ///await _unitOfWork.CommitAsync();
                await _unitOfWork.Categories.AddAsync(_mapper.Map<Category>(model));
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

        [Authorize(Permissions.Categories.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = _mapper.Map<EditCategoryVM>(await _unitOfWork.Categories.GetByIdAsync(id));
                model.Departments = await _unitOfWork.Departments.GetAllAsync();
                return View(model);
            }
            catch (Exception)
            {
                return View();
            }
        }

        [Authorize(Permissions.Categories.Edit)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCategoryVM model)
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

                    model.ImgPath = await _fileUpload.
                        UploadFileAsync(this.ControllerContext.RouteData.Values["Controller"].ToString(), model.ImgFile);
                }

                _unitOfWork.Categories.Update(_mapper.Map<Category>(model));
                await _unitOfWork.CommitAsync();

                _toastNotification.AddSuccessToastMessage(Alerts.UpdateSucces);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        [Authorize(Permissions.Categories.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = _mapper.Map<DeleteCategoryVM>(await _unitOfWork.Categories.GetByIdAsync(id));
                model.Department = await _unitOfWork.Departments.GetByIdAsync(model.DepartmentId);
                return View(model);
            }
            catch (Exception)
            {
                return View();
            }
        }


        [Authorize(Permissions.Categories.Delete)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteCategoryVM model)
        {
            try
            {
                //Remove Existing Image
                System.IO.File.Delete($"wwwroot/{model.ImgPath}");

                _unitOfWork.Categories.Delete(_mapper.Map<Category>(model));
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
