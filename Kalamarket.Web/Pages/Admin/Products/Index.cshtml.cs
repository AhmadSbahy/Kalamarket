using Kalamarket.Core.DTOs.Admin.Product;
using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Classes;
using Kalamarket.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kalamarket.Web.Pages.Admin.Product
{
	[PermissionChecker(6)]
	public class IndexModel : PageModel
    {
        private IAdminService _adminSrvice;

        public IndexModel(IAdminService admin)
        {
            _adminSrvice = admin;
        }
        public List<ShowProductViewModel> Products { get; set; }
        public void OnGet(int pageId = 1,int take = 1,string name = "",string ordertype = "")
        {
            Products = _adminSrvice.GetAllProductForAdmin(pageId,take,name,ordertype).Item1;
            ViewData["count"] = _adminSrvice.GetAllProductForAdmin().Item2;
            ViewData["pageId"] = pageId;
        }
    }
}
