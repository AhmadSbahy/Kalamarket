using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.Groups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kalamarket.Web.Pages.Admin.Groups
{
	[PermissionChecker(16)]
	public class EditModel : PageModel
    {
		private IAdminService _adminsrvice;
		public EditModel(IAdminService admin)
		{
			_adminsrvice = admin;
		}
		[BindProperty]
		public Group Group { get; set; }
		public void OnGet(int id)
		{
			Group = _adminsrvice.GetGroupById(id);
		}
		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			_adminsrvice.UpdateGroup(Group);
			return RedirectToPage("Index");
		}
	}
}
