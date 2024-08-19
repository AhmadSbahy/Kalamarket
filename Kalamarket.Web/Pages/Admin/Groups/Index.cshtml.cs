using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.Groups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kalamarket.Web.Pages.Admin.Groups
{
	[PermissionChecker(14)]
	public class IndexModel : PageModel
    {
        private IAdminService _adminsrvice;
        public IndexModel(IAdminService admin)
        {
            _adminsrvice = admin;
        }
        public List<Group> Groups { get; set; }
        public void OnGet()
        {
            Groups = _adminsrvice.GetAllGroupsForAdmin();
        }
    }
}
