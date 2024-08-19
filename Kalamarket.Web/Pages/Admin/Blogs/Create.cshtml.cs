using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Blog;
using Kalamarket.Core.Generator;
using Kalamarket.DataLayer.Eintitys.Product;
using Toplearn.Core.Convartor;
using Kalamarket.Core.Service.Interface;
using Kalamarket.Core.Security;

namespace Kalamarket.Web.Pages.Admin.Blogs
{
	[PermissionChecker(23)]
	public class CreateModel : PageModel
    {
        private readonly Kalamarket.DataLayer.Context.KalamarketContext _context;
        private IUserSrvice _userSrvice;
        public CreateModel(Kalamarket.DataLayer.Context.KalamarketContext context,IUserSrvice user)
        {
            _context = context;
            _userSrvice = user;
        }

        public IActionResult OnGet()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public Blog Blog { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (!ModelState.IsValid || file == null)
            {
                ModelState.AddModelError("Blog.BlogTitle", "مقادير وارد شده معتبر نمى باشند");
                return Page();
            }
            string ImageName = NameGenerator.GenerateUniqName() + Path.GetExtension(file.FileName);
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Blog", ImageName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            Blog.ImageName = ImageName;
            Blog.UserId = _userSrvice.GetUserIdByUserName(User.Identity.Name);
            Blog.CreateDate = DateTime.Now;
            _context.Blogs.Add(Blog);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
