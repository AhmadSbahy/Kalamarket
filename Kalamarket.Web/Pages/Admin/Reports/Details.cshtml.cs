using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.ContactUs;
using Kalamarket.Core.Security;

namespace Kalamarket.Web.Pages.Admin.Reports
{
	[PermissionChecker(26)]
	public class DetailsModel : PageModel
    {
        private readonly Kalamarket.DataLayer.Context.KalamarketContext _context;

        public DetailsModel(Kalamarket.DataLayer.Context.KalamarketContext context)
        {
            _context = context;
        }

        public ContactUs ContactUs { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContactUs = await _context.ContactUs
                .Include(c => c.Subject)
                .Include(c => c.User).FirstOrDefaultAsync(m => m.ContactId == id);

            if (ContactUs == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
