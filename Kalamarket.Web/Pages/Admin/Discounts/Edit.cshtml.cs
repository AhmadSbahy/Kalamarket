using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace Kalamarket.Web.Pages.Admin.Discounts
{
	[PermissionChecker(20)]
	public class EditModel : PageModel
    {
		private IAdminService _adminsrvice;
		public EditModel(IAdminService admin)
		{
			_adminsrvice = admin;
		}
		[BindProperty]
        public Discount Discount { get; set; }
        public void OnGet(int id)
        {
            Discount = _adminsrvice.GetDiscountById(id);
		}
		public IActionResult OnPost(string? stDate, string? edDate)
		{
			if (stDate != null)
			{
				string[] std = stDate.Split('/');
				Discount.StartDate = new DateTime(int.Parse(std[0]), int.Parse(std[1]), int.Parse(std[2]), new PersianCalendar());
			}
			if (edDate != null)
			{
				string[] edd = edDate.Split('/');
				Discount.EndDate = new DateTime(int.Parse(edd[0]), int.Parse(edd[1]), int.Parse(edd[2]), new PersianCalendar());
			}
			if (!ModelState.IsValid)
			{
				return Page();
			}
			if (_adminsrvice.IsCodeExsist(Discount.DiscountCode,Discount.DiscountId))
			{
				ModelState.AddModelError("Discount.DiscountCode", "كد وارد شده قبلا وجود داشته");
				return Page();
			}
			_adminsrvice.UpdateDiscount(Discount);
			return RedirectToPage("Index");
		}
	}
}
