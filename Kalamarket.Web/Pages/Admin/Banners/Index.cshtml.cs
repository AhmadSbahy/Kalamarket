using Kalamarket.Core.DTOs.Admin.Users;
using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.Banners;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kalamarket.Web.Pages.Admin.Banners
{
	[PermissionChecker(28)]
    public class IndexModel : PageModel
    {
		private IAdminService _adminsrvice;
		public IndexModel(IAdminService admin)
		{
			_adminsrvice = admin;
		}
		public List<Banner> ListBanner { get; set; }
		public void OnGet()
		{
			ListBanner = _adminsrvice.GetAllBanners();
		}
	}
}
