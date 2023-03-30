using Ecommerce.Areas.Admin.ViewModels.UsersViewModels;
using Ecommerce.Constants;

namespace Ecommerce.Areas.Admin.ViewModels.RolesViewModels
{
    public class RolePermissionsViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<CheckBoxViewModel> RoleCalims { get; set; }
        public bool ToggleAllCbx { get; set; }
        public int ModulesCount { get; set; } = Enum.GetNames(typeof(Modules)).Count();
    }
}
