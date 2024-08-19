using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Blog;
using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Azure;
using Kalamarket.Core.Service.Classes;

namespace Kalamarket.Web.Pages.Admin.Blogs
{
	[PermissionChecker(22)]
	public class IndexModel : PageModel
    {
        private readonly Kalamarket.DataLayer.Context.KalamarketContext _context;
        private IAdminService _adminSrvice;

        public IndexModel(Kalamarket.DataLayer.Context.KalamarketContext context,IAdminService admin)
        {
            _context = context;
            _adminSrvice = admin;
        }

        public IList<Blog> Blog { get;set; }

        public async Task OnGetAsync(int pageId =1,int take = 1)
        {
            Blog = _adminSrvice.GetBlogs(pageId, take).Item1;
            ViewData["count"] = _adminSrvice.GetBlogs().Item2;
            ViewData["pageId"] = pageId;
        }
    }
}
