using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.Groups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kalamarket.Web.Pages.Admin.Groups
{
	[PermissionChecker(15)]
	public class AddModel : PageModel
    {
		private IAdminService _adminsrvice;
		public AddModel(IAdminService admin)
		{
			_adminsrvice = admin;
		}
		[BindProperty]
		public Group Group { get; set; }
        public void OnGet(int? id)
        {
			Group = new Group()
			{
				GroupParentId = id
			};
		}
		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			_adminsrvice.AddGroup(Group);
			return RedirectToPage("Index");
		}
    }
}
