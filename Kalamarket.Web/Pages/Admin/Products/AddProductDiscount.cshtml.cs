using System.Globalization;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kalamarket.Web.Pages.Admin.Products
{
	public class AddProductDiscountModel : PageModel
	{
		private readonly IAdminService _adminService;

		public AddProductDiscountModel(IAdminService adminService)
		{
			_adminService = adminService;
		}
		
		[BindProperty]
		public ProductDiscount ProductDiscount { get; set; }
		
		public void OnGet(int id)
		{ 
			// اطمینان از مقداردهی
			if (ProductDiscount == null)
			{
				ProductDiscount = new ProductDiscount();
			}
			ProductDiscount.ProductId = id;
		}


		public IActionResult OnPost(string stDate, string edDate)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			string[] std = stDate.Split('/');
			ProductDiscount.StartDate = new DateTime(int.Parse(std[0]), int.Parse(std[1]), int.Parse(std[2]), new PersianCalendar());

			string[] edd = edDate.Split('/');
			ProductDiscount.EndDate = new DateTime(int.Parse(edd[0]), int.Parse(edd[1]), int.Parse(edd[2]), new PersianCalendar());

			_adminService.AddProductDiscount(ProductDiscount);
			return RedirectToPage("ProductDiscount");
		}
	}
}
