// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Ecommerce.Constants;
using Ecommerce.CustomValidations;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace Ecommerce.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IToastNotification _toastNotification;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IToastNotification toastNotification)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _toastNotification = toastNotification;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            
            //Adding Custom field First Name
            [Required(ErrorMessage = "Input can not be empty")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            //Adding Custom field Last Name
            [Required(ErrorMessage = "Input can not be empty")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Phone]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            //Adding Custom field Profile Picture
            [Display(Name = "Profile Picture")]
            public byte[] ProfilePicture { get; set; }
        }
            
        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            //Adding Custom Field To User Table  (First Name, Last Name, Profile Picture)
            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName =user.LastName,
                PhoneNumber = phoneNumber,
                ProfilePicture = user.ProfilePicture
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            //Uploading Profile Picture To Database
            ///Check File Size Is Less Than 4MB
            ///var fileSize = Request.Form.Files.FirstOrDefault().Length
            ///var requiredFileSize = FileSize.ImgFileSize;
            ///if (FileSize.IsValidSize(fileSize, FileSize.ImgFileSize))
            if (FileSizeValidation.IsValidSize(Request.Form.Files.FirstOrDefault().Length, FileSize.ImgFileSize))
            {
                ///Getting File Extension (Type)
                ///extension = Path.GetExtension(Request.Form.Files.FirstOrDefault().FileName).TrimStart('.');
                ///Checking File Extinsion (jpg, jpeg, png or bmp)
                if (ExtensionValidation.IsImage(Path.GetExtension(Request.Form.Files.FirstOrDefault().FileName).TrimStart('.')))
                {
                    //Copy Profile Picture To Database
                    using (var ProfilePicMemoryStream = new MemoryStream())
                    {
                        await Request.Form.Files.FirstOrDefault().CopyToAsync(ProfilePicMemoryStream);
                        user.ProfilePicture = ProfilePicMemoryStream.ToArray();
                    }
                    await _userManager.UpdateAsync(user);
                }
                else
                    _toastNotification.AddErrorToastMessage(Alerts.ErrorMsgImgExtension);
            }
            else
                _toastNotification.AddErrorToastMessage("Exceeded Image Maximum Size 4MB");

            //Updating First Name If Changed
            if (user.FirstName != Input.FirstName)
            {
                user.FirstName = Input.FirstName;
                await _userManager.UpdateAsync(user);
            }

            //Updating Last Name If Changed
            if (user.LastName != Input.LastName)
            {
                user.LastName = Input.LastName;
                await _userManager.UpdateAsync(user);
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
