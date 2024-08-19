using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Blog;
using System.Reflection.Metadata;
using Kalamarket.Core.Security;

namespace Kalamarket.Web.Pages.Admin.Blogs
{
	[PermissionChecker(25)]
	public class DeleteModel : PageModel
	{
		private readonly Kalamarket.DataLayer.Context.KalamarketContext _context;

		public DeleteModel(Kalamarket.DataLayer.Context.KalamarketContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Blog Blog { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Blog = await _context.Blogs
				.Include(b => b.User).FirstOrDefaultAsync(m => m.BlogId == id);

			if (Blog == null)
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

			Blog = await _context.Blogs.Include(b => b.BlogComments).FirstOrDefaultAsync(p => p.BlogId == id);
            if (Blog != null)
			{
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Blog", Blog.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                if (Blog.BlogComments != null && Blog.BlogComments.Any() && Blog.BlogComments.Count > 0)
				{	
					foreach (var item in Blog.BlogComments)
					{
						_context.BlogComments.Remove(item);
					}
				}
				_context.Blogs.Remove(Blog);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
