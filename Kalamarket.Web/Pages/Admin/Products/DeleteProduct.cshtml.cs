using Kalamarket.Core.DTOs.Admin.Product;
using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Kalamarket.Web.Pages.Admin.ManageUsers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kalamarket.Web.Pages.Admin.Product
{
	[PermissionChecker(9)]
	public class DeleteProductModel : PageModel
    {
		private IAdminService _adminsrvice;
		public DeleteProductModel(IAdminService admin)
		{
			_adminsrvice = admin;
		}
		public DeleteProductViewModel DeleteeModel { get; set; }
		public void OnGet(int id)
		{
			DeleteeModel = _adminsrvice.GetProductForDelete(id);
		}
		public IActionResult OnPost(int id)
		{
			 _adminsrvice.DeleteProductImage(id);
			_adminsrvice.DeletProduct(id);
			return RedirectToPage("Index");
		}
    }
}
