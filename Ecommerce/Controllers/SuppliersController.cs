using AutoMapper;
using Ecommerce.Constants;
using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Ecommerce.ViewModels.SupplierViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Ecommerce.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;

        public SuppliersController(IUnitOfWork unitOfWork, IMapper mapper, IToastNotification toastNotification)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                ///var model = _mapper.Map<List<Supplier>, List<IndexSupplierViewModel>>(
                ///    await _unitOfWork.Suppliers.FindAllAsync(
                ///        p => p.Id != null, new string[] { "SupplierPhones" })
                ///    );
                ///return View(model);
                return View(_mapper.Map<List<Supplier>, List<IndexSupplierVM>>(await _unitOfWork.Suppliers.FindAllAsync(p => p.Id != null, new string[] { "SupplierPhones" })));
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
                return View(new AddSupplierVM());
            }
            catch (Exception)
            {
                return View();
            }
        }


        [Authorize(Permissions.Brands.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddSupplierVM model)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage(Alerts.ModelStateErrorMsg);
                return View(new AddSupplierVM());
            }

            ///Map AddSupplierViewModel Object To a Supplier Object
            ///Add Supplier Object To Database & Save Changes
            ///var supplier = _mapper.Map<Supplier>(model);
            ///await _unitOfWork.Supplier.AddAsync(supplier);
            ///await _unitOfWork.CommitAsync();
            await _unitOfWork.Suppliers.AddAsync(_mapper.Map<Supplier>(model));
            await _unitOfWork.CommitAsync();

            //Add Sucess Notification
            _toastNotification.AddSuccessToastMessage(Alerts.AddedSucces);

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Permissions.Brands.Edit)]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                 ///var model = _mapper.Map<EditSupplierVM>(
                 ///await _unitOfWork.Suppliers.FindAsync(
                 ///p => p.Id == id, new string[] { "SupplierPhones" })
                 ///   );
                 ///return View(model);
                return View(_mapper.Map<EditSupplierVM>(await _unitOfWork.Suppliers.FindAsync(p => p.Id == id, new string[] { "SupplierPhones" })));
            }
            catch (Exception)
            {
                return View();
            }
        }


        [Authorize(Permissions.Brands.Edit)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditSupplierVM model)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage(Alerts.ModelStateErrorMsg);
                return View();
            }

            ///Map EditSupplierViewModel Object To a Supplier Object
            ///Add Supplier Object To Database & Save Changes
            ///var supplier = _mapper.Map<Supplier>(model);
            ///await _unitOfWork.Supplier.Update(supplier);
            ///await _unitOfWork.CommitAsync();
            for (int i = 0; i < model.SupplierPhones.Count; i++)
            {
                model.SupplierPhones[i].SupplierId = model.Id;
                model.SupplierPhones[i].Supplier = await _unitOfWork.Suppliers.GetByIdAsync(model.Id);
            }

            var x = model.SupplierPhones;
            
            _unitOfWork.Suppliers.Update(_mapper.Map<Supplier>(model));
            await _unitOfWork.CommitAsync();

            //Add Sucess Notification
            _toastNotification.AddSuccessToastMessage(Alerts.UpdateSucces);

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Permissions.Brands.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var model = _mapper.Map<DeleteSupplierVM>(await _unitOfWork.Suppliers.FindAsync(p => p.Id == id, new string[] { "SupplierPhones" }));
            return View(model);
        }


        [Authorize(Permissions.Brands.Delete)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteSupplierVM model)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage(Alerts.ModelStateErrorMsg);
                return View();
            }

            _unitOfWork.Suppliers.Delete(_mapper.Map<Supplier>(model));
            await _unitOfWork.CommitAsync();

            //Add Sucess Notification
            _toastNotification.AddSuccessToastMessage(Alerts.UpdateSucces);

            return RedirectToAction(nameof(Index));
        }
    }
}
