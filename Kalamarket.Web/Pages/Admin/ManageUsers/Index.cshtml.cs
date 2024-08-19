using Kalamarket.Core.DTOs.Admin.Users;
using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kalamarket.Web.Pages.Admin.ManageUsers
{
	[PermissionChecker(2)]
	public class IndexModel : PageModel
    {
        private IAdminService _adminsrvice;
        public IndexModel(IAdminService admin)
        {
            _adminsrvice = admin;
        }
        public List<UserListViewModel> ListUsers { get; set; }
        public void OnGet(int pageId=1,string email = "",string number = "",string username = "",int take = 1)
        {
            ListUsers = _adminsrvice.GetAllUserForAdmin(pageId,email,number,username, take).Item1;
            ViewData["count"] = _adminsrvice.GetAllUserForAdmin().Item2;
            ViewData["pageId"] = pageId;
        }
    }
}
