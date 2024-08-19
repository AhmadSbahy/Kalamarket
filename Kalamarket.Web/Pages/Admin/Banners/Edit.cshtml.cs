using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Banners;
using Kalamarket.Core.Generator;
using Kalamarket.DataLayer.Eintitys.Blog;
using Kalamarket.Core.Security;

namespace Kalamarket.Web.Pages.Admin.Banners
{
	[PermissionChecker(30)]
	public class EditModel : PageModel
    {
        private readonly Kalamarket.DataLayer.Context.KalamarketContext _context;

        public EditModel(Kalamarket.DataLayer.Context.KalamarketContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (file != null)
            {
                string delPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Banners", Banner.ImageName);
                if (System.IO.File.Exists(delPath))
                {
                    System.IO.File.Delete(delPath);
                }

                string ImageName = NameGenerator.GenerateUniqName() + Path.GetExtension(file.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Banners", ImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                Banner.ImageName = ImageName;

            }

            _context.Banners.Update(Banner);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BannerExists(Banner.BannerId))
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

        private bool BannerExists(int id)
        {
            return _context.Banners.Any(e => e.BannerId == id);
        }
    }
}
