using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kalamarket.Web.Pages.Admin.ManageUsers
{
	[PermissionChecker(5)]
	public class DeleteModel : PageModel
    {
        private IAdminService _adminsrvice;
        public DeleteModel(IAdminService admin)
        {
            _adminsrvice = admin;
        }
        public User User { get; set; }
        public void OnGet(int id)
        {
            User = _adminsrvice.GetUserByIdForAdmin(id);
        }
        public IActionResult Onpost(int id)
        {
            _adminsrvice.DeleteUserFromAdmin(id);

			return RedirectToPage("Index");
        }
    }
}
