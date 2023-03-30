using AutoMapper;
using Ecommerce.Constants;
using Ecommerce.CustomValidations;
using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Ecommerce.ViewModels.DepartmentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Ecommerce.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileUpload _fileUpload;
        private readonly  IToastNotification _toastNotification;

        public DepartmentsController(IUnitOfWork unitOfWork, IMapper mapper, 
                IFileUpload fileUpload, IToastNotification toastNotification)
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
                return View(_mapper.Map<List<Department>, List<IndexDepartmentVM>>(await _unitOfWork.Departments.GetAllAsync()));

            }
            catch (Exception)
            {

                return View();
            }
        }


        [Authorize(Permissions.Departments.Create)]
        public async Task<IActionResult> Create()
        {
            try
            {
                return View(new AddDepartmentVM());
            }
            catch (Exception)
            {

                return View();
            }
        }


        [Authorize(Permissions.Departments.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddDepartmentVM model)
        {
            try
            {
                //Re-render The View When ModelState Validation Failed.
                if (!ModelState.IsValid)
                {
                    _toastNotification.AddErrorToastMessage(Alerts.ModelStateErrorMsg);
                    return View(new AddDepartmentVM());
                }

                ///Check If Image Has Valid Extension
                ///var extension = Path.GetExtension(model.ImgFile.FileName.TrimStart('.');
                ///if(Extension.IsImage(extension))
                if (!ExtensionValidation.IsImage(Path.GetExtension(model.ImgFile.FileName).TrimStart('.')))
                {
                    _toastNotification.AddErrorToastMessage(Alerts.ErrorMsgImgExtension);
                    return View(new AddDepartmentVM());
                }

                ///Check File Size Is Less Than 4MB
                ///fileSize = model.ImgFile.Length
                ///if (FileSize.IsValidSize(fileSize, 4))
                if (!FileSizeValidation.IsValidSize(model.ImgFile.Length, FileSize.ImgFileSize))
                {
                    _toastNotification.AddErrorToastMessage(Alerts.InavlidImgFileSize);
                    return View(new AddDepartmentVM());
                }

                //Set Path Of The Image To ImgPath Property
                model.ImgPath = await _fileUpload
                    .UploadFileAsync(this.ControllerContext.RouteData.Values["Controller"].ToString()
                        , model.ImgFile);

                ///Map AddDepartmentViewModel Object To a Department Object
                ///Add Department Object To Database & Save Changes
                ///var department = _mapper.Map<Department>(model);
                ///await _unitOfWork.Departments.AddAsync(department);
                ///await _unitOfWork.CommitAsync();
                await _unitOfWork.Departments.AddAsync(_mapper.Map<Department>(model));
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


        [Authorize(Permissions.Departments.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                return View(_mapper.Map<EditDepartmentVM>(await _unitOfWork.Departments.GetByIdAsync(id)));
            }
            catch (Exception)
            {
                return View();
            }
        }


        [Authorize(Permissions.Departments.Edit)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditDepartmentVM model)
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

                _unitOfWork.Departments.Update(_mapper.Map<Department>(model));
                await _unitOfWork.CommitAsync();

                _toastNotification.AddSuccessToastMessage(Alerts.UpdateSucces);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }


        [Authorize(Permissions.Departments.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return View(_mapper.Map<DeleteDepartmentVM>(await _unitOfWork.Departments.GetByIdAsync(id)));
            }
            catch (Exception)
            {
                return View();
            }
        }


        [Authorize(Permissions.Departments.Delete)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteDepartmentVM model)
        {
            try
            {
                //Remove Existing Image
                System.IO.File.Delete($"wwwroot/{model.ImgPath}");

                _unitOfWork.Departments.Delete(_mapper.Map<Department>(model));
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