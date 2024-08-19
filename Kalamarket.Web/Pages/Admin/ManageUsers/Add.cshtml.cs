using Kalamarket.Core.DTOs.Admin.Users;
using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

namespace Kalamarket.Web.Pages.Admin.ManageUsers
{
	[PermissionChecker(3)]
	public class AddModel : PageModel
    {
        private IAdminService _adminsrvice;
        private IPermissionSrvice _permitionsrvice;
        public AddModel(IAdminService admin,IPermissionSrvice srvice)
        {
            _adminsrvice = admin;
            _permitionsrvice = srvice;
        }
        [BindProperty]
        public AddUserFromAdminViewModel User { get; set; }
        public void OnGet()
        {
            ViewData["Roles"] = _permitionsrvice.GetRoles();
        }
        public IActionResult Onpost(List<int> rolesId)
        {
			ViewData["Roles"] = _permitionsrvice.GetRoles();

			if (_adminsrvice.IsEmailOrUsernameExist(User.Email, User.Username))
            {
                ModelState.AddModelError("User.Email", "ايميل يا نام كاربرى تكرارى مى باشد");
                return Page();
            }
            if (User.Phonenmumber.Length>11 ||User.Phonenmumber.Length<11)
            {
				ModelState.AddModelError("User.Phonenmumber", "شماره تماس وارد شده معتبر نمى باشد");
				return Page();
			}

           int userId = _adminsrvice.AddUserFromAdmin(User);
            _permitionsrvice.AddUserRoles(rolesId, userId);
			return RedirectToPage("Index");
        }

    }
}
