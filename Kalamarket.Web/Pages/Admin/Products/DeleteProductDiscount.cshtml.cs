using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kalamarket.Web.Pages.Admin.Products
{
    public class DeleteProductDiscountModel : PageModel
    {
		private readonly IAdminService _adminService;

		public DeleteProductDiscountModel(IAdminService adminService)
		{
			_adminService = adminService;
		}

		public void OnGet(int id)
		{
			ProductDiscount = _adminService.GetProductDiscountById(id);
		}
		public ProductDiscount ProductDiscount { get; set; }

		public IActionResult OnPost(int id)
		{
			_adminService.DeleteProductDiscount(id);
			return RedirectToPage("ProductDiscount");
		}
	}
}
