using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Banners;
using Kalamarket.Core.Generator;
using Kalamarket.DataLayer.Eintitys.Blog;
using Kalamarket.Core.Security;

namespace Kalamarket.Web.Pages.Admin.Banners
{
	[PermissionChecker(29)]
	public class AddModel : PageModel
    {
        private readonly Kalamarket.DataLayer.Context.KalamarketContext _context;

        public AddModel(KalamarketContext context)
        {
	        _context = context;	
        }
        public IActionResult OnGet()
        {
            return Page();
        }
		[BindProperty]
        public Banner Banner { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile? file)
        {
			if (!ModelState.IsValid || file == null)
			{
				ModelState.AddModelError("Banner.ImageUrl", "مقادير وارد شده معتبر نمى باشند");
				return Page();
			}
			string ImageName = NameGenerator.GenerateUniqName() + Path.GetExtension(file.FileName);
			string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Banners", ImageName);

			using (var stream = new FileStream(imagePath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			Banner.ImageName = ImageName;

			_context.Banners.Add(Banner);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
