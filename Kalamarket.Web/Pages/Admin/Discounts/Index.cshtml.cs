using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Classes;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kalamarket.Web.Pages.Admin.Discounts
{
	[PermissionChecker(18)]
	public class IndexModel : PageModel
    {
		private IAdminService _adminsrvice;
		public IndexModel(IAdminService admin)
		{
			_adminsrvice = admin;
		}
        public List<Discount> Discounts { get; set; }
        public void OnGet(int pageId = 1,int take = 1)
        {
            Discounts = _adminsrvice.GetAllDiscounts(pageId,take).Item1;
            ViewData["count"] = _adminsrvice.GetAllDiscounts().Item2;
            ViewData["pageId"] = pageId;
        }
    }
}
