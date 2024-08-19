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
    public class DeleteModel : PageModel
    {
        private readonly Kalamarket.DataLayer.Context.KalamarketContext _context;

        public DeleteModel(Kalamarket.DataLayer.Context.KalamarketContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderStatus = await _context.OrderStatus.FindAsync(id);

            if (OrderStatus != null)
            {
                _context.OrderStatus.Remove(OrderStatus);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
