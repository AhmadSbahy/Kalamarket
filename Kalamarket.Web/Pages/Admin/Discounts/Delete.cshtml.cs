using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Order;
using Kalamarket.Core.Security;

namespace Kalamarket.Web.Pages.Admin.Discounts
{
	[PermissionChecker(21)]
	public class DeleteModel : PageModel
	{
		private readonly Kalamarket.DataLayer.Context.KalamarketContext _context;

		public DeleteModel(Kalamarket.DataLayer.Context.KalamarketContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Discount Discount { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Discount = await _context.Discounts.FirstOrDefaultAsync(m => m.DiscountId == id);

			if (Discount == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Discount = await _context.Discounts
	.Include(p => p.UserDiscounts)
	.SingleOrDefaultAsync(discount => discount.DiscountId == id);

			if (Discount != null)
			{
				if (Discount.UserDiscounts != null && Discount.UserDiscounts.Any())
				{
					foreach (var item in Discount.UserDiscounts)
					{
						_context.UserDiscounts.Remove(item);
					}
				}
				_context.Discounts.Remove(Discount);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
