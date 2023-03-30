using Ecommerce.Areas.Admin.ViewModels.RolesViewModels;
using Ecommerce.Areas.Admin.ViewModels.UsersViewModels;
using Ecommerce.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Security.Claims;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IToastNotification _toastNotification;

        public RolesController(RoleManager<IdentityRole> userManager, IToastNotification toastNotification)
        {
            _roleManager = userManager;
            _toastNotification = toastNotification;
        }

        [Authorize(Permissions.Roles.View)]
        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

        [Authorize(Permissions.Roles.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddRoleViewModel model)
        {
            //Reload Index View 
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), await _roleManager.Roles.ToListAsync());
            }

            //Validate that input in not duplicated
            //Show Error Message to User
            if (await _roleManager.RoleExistsAsync(model.Name))
            {
                ModelState.AddModelError("Name", "Role Already Exists");
                return View(nameof(Index), await _roleManager.Roles.ToListAsync());
            }

            //Add new Role
            await _roleManager.CreateAsync(new IdentityRole(model.Name));

            _toastNotification.AddSuccessToastMessage("Role Added Successfully");

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Roles.Edit)]
        public async Task<IActionResult> ManagePermissions(string Id)
        {
            //Get Role
            var role = await _roleManager.FindByIdAsync(Id);

            //Check If Role not Exists => NotFound
            if (role == null)
                return NotFound();

            //Get Claims Of The Role
            var roleClaims = _roleManager.GetClaimsAsync(role).Result.Select(c => c.Value).ToList();

            //Get All Claims
            var allClaims = Permissions.GenerateAllPermissions().Select(c => new CheckBoxViewModel
            {
                DisplayValue = c
            }).ToList();

            //Get All Intersected Claims (allClaims & roleClaims) and assigned it by true value
            foreach (var claim in allClaims)
                if (roleClaims.Any(c => c == claim.DisplayValue))
                    claim.IsSelected = true;

            //Assign Values To RolePermissionsViewModel To Be Sent To ManageRoles View
            var model = new RolePermissionsViewModel
            {
                RoleId = Id,
                RoleName = role.Name,
                RoleCalims = allClaims
            };

            return View(model);
        }

        [Authorize(Permissions.Roles.Edit)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManagePermissions(RolePermissionsViewModel model)
        {
            //Get Role
            var role = await _roleManager.FindByIdAsync(model.RoleId);

            //Check If Role not Exists => NotFound
            if (role == null)
                return NotFound();

            //Remove Claims Of The Role 
            foreach(var claim in await _roleManager.GetClaimsAsync(role))
                await _roleManager.RemoveClaimAsync(role, claim);

            //Assign Selected Claims To The Role
            foreach (var claim in model.RoleCalims.Where(c => c.IsSelected).ToList())
                await _roleManager.AddClaimAsync(role, new Claim("Permission", claim.DisplayValue));

             
            return RedirectToAction(nameof(Index));
        }
    }
}
