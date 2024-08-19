using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Order;

namespace Kalamarket.Web.Pages.Admin.OrderStatus
{
    public class CreateModel : PageModel
    {
        private readonly Kalamarket.DataLayer.Context.KalamarketContext _context;

        public CreateModel(Kalamarket.DataLayer.Context.KalamarketContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DataLayer.Eintitys.Order.OrderStatus OrderStatus { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OrderStatus.Add(OrderStatus);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
