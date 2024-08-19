using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Blog;
using Kalamarket.Core.Service.Interface;
using Kalamarket.Core.Generator;
using Kalamarket.Core.Security;

namespace Kalamarket.Web.Pages.Admin.Blogs
{
	[PermissionChecker(24)]
	public class EditModel : PageModel
	{

		private readonly Kalamarket.DataLayer.Context.KalamarketContext _context;
		private IAdminService _adminSrvice;
		public EditModel(Kalamarket.DataLayer.Context.KalamarketContext context,IAdminService admin)
		{
			_context = context;
			_adminSrvice = admin;
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
			ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
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
				_adminSrvice.DeleteBlogImage(Blog);
				string ImageName = NameGenerator.GenerateUniqName() + Path.GetExtension(file.FileName);
				string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Blog", ImageName);

				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					file.CopyTo(stream);
				}
				Blog.ImageName = ImageName;
			}

			_context.Blogs.Update(Blog);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BlogExists(Blog.BlogId))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("Index");
		}

		private bool BlogExists(int id)
		{
			return _context.Blogs.Any(e => e.BlogId == id);
		}
	}
}
