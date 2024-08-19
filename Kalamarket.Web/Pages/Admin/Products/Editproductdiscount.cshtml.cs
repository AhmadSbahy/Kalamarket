using System.Globalization;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kalamarket.Web.Pages.Admin.Products
{
	public class EditproductdiscountModel : PageModel
	{
		private readonly IAdminService _adminService;

		public EditproductdiscountModel(IAdminService adminService)
		{
			_adminService = adminService;
		}

		public void OnGet(int id)
		{
			ProductDiscount = _adminService.GetProductDiscountById(id);
		}
		[BindProperty]
		public ProductDiscount ProductDiscount { get; set; }

		public IActionResult OnPost(string stDate, string edDate)
		{

			string[] std = stDate.Split('/');
			ProductDiscount.StartDate = new DateTime(int.Parse(std[0]), int.Parse(std[1]), int.Parse(std[2]), new PersianCalendar());


			string[] edd = edDate.Split('/');
			ProductDiscount.EndDate = new DateTime(int.Parse(edd[0]), int.Parse(edd[1]), int.Parse(edd[2]), new PersianCalendar());

			if (!ModelState.IsValid)
			{
				return Page();
			}
			_adminService.UpdateProductDiscount(ProductDiscount);
			return RedirectToPage("ProductDiscount");
		}
	}
}
