using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Banners;
using Kalamarket.Core.Security;

namespace Kalamarket.Web.Pages.Admin.Banners
{
	[PermissionChecker(28)]
	public class DetalisModel : PageModel
    {
        private readonly Kalamarket.DataLayer.Context.KalamarketContext _context;

        public DetalisModel(Kalamarket.DataLayer.Context.KalamarketContext context)
        {
            _context = context;
        }

        public Banner Banner { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Banner = await _context.Banners.FirstOrDefaultAsync(m => m.BannerId == id);

            if (Banner == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
