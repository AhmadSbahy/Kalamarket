using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Order;

namespace Kalamarket.Web.Pages.Admin.OrderStatus
{
    public class EditModel : PageModel
    {
        private readonly Kalamarket.DataLayer.Context.KalamarketContext _context;

        public EditModel(Kalamarket.DataLayer.Context.KalamarketContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OrderStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStatusExists(OrderStatus.OrderStatusId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrderStatusExists(int id)
        {
            return _context.OrderStatus.Any(e => e.OrderStatusId == id);
        }
    }
}
