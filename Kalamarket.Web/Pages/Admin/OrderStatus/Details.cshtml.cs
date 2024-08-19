using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Order;

namespace Kalamarket.Web.Pages.Admin.OrderStatus
{
    public class DetailsModel : PageModel
    {
        private readonly Kalamarket.DataLayer.Context.KalamarketContext _context;

        public DetailsModel(Kalamarket.DataLayer.Context.KalamarketContext context)
        {
            _context = context;
        }

        public DataLayer.Eintitys.Order.OrderStatus OrderStatus { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderStatus = await _context.OrderStatus.FirstOrDefaultAsync(m => m.OrderStatusId == id);

            if (OrderStatus == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
