using Kalamarket.Core.DTOs.Admin.Users;
using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kalamarket.Web.Pages.Admin.ManageUsers
{
	[PermissionChecker(4)]
	public class EditModel : PageModel
	{
		private IAdminService _adminsrvice;
		private IPermissionSrvice _permitionsrvice;
		public EditModel(IAdminService admin, IPermissionSrvice srvice)
		{
			_adminsrvice = admin;
			_permitionsrvice = srvice;
		}
		[BindProperty]
		public User User { get; set; }
		public IActionResult OnGet(int id)
		{
			User = _adminsrvice.GetUserByIdForAdmin(id);
			if(User != null)
			{
				ViewData["Roles"] = _permitionsrvice.GetRoles();
				ViewData["UserRoles"] = _permitionsrvice.GetUserRoles(User.UserName);
				return Page();
			}
			else
			{
				return NotFound();
			}	
		}

		public IActionResult OnPost(List<int> rolesId)
		{
			ViewData["Roles"] = _permitionsrvice.GetRoles();
			if (string.IsNullOrEmpty(User.Password))
			{
				User.Password = _adminsrvice.GetPasswordUserById(User.UserId);
			}
			User.Password = PasswordHelper.EncodePasswordMd5(User.Password);
			_adminsrvice.UpdateUserFromAdmin(User);

			_permitionsrvice.UpdateUserRole(User.UserName, rolesId);

			return RedirectToPage("Index");
		}
	}
}
