using AutoMapper;
using Ecommerce.Areas.Admin.ViewModels.RolesViewModels;
using Ecommerce.Areas.Admin.ViewModels.UsersViewModels;
using Ecommerce.Constants;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Permissions.Users.View)]
        public async Task<IActionResult> Index()
        {
            //Assign Values To UserRolesViewModel To Be Sent To Index View
            var users = await _userManager.Users.Select(user => new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            }).ToListAsync();

            return View(users);
        }

        [Authorize(Permissions.Users.Edit)]
        public async Task<IActionResult> ManageRoles(string Id)
        {
            //Get User
            var user = await _userManager.FindByIdAsync(Id);

            //Check If User not Exists => NotFound
            if (user == null)
                return NotFound();

            //Get All Roles
            var roles = await _roleManager.Roles.ToListAsync();

            //Assign Values To UserRolesViewModel To Be Sent To ManageRoles View
            var model = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(rol => new CheckBoxViewModel
                {
                    DisplayValue = rol.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, rol.Name).Result
                }).ToList()
            };

            return View(model);
        }

        [Authorize(Permissions.Users.Edit)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesViewModel model)
        {
            //Check User Exists
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound();

            //Get All User Roles
            var roles = await _userManager.GetRolesAsync(user);

            //When User Assigned To Role And This Role Still Checked => No Action Needed
            //When User Isn't Assigned To Role And This Role Still UnChecked => No Action Needed
            //When User Assigned To Role And This Role UnChecked => Remove This Role From The User
            //When User Isn't Assigned To Role And This Role Checked => Add This Role To The User
            foreach(var role in model.Roles)
            {
                if (roles.Any(r => r == role.DisplayValue) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.DisplayValue);

                if (!roles.Any(r => r == role.DisplayValue) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.DisplayValue);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}