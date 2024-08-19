using Kalamarket.Core.DTOs.Admin.Index;
using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kalamarket.Web.Pages.Admin
{
	public class IndexModel : PageModel
    {
		private IAdminService _adminsrvice;
		public IndexModel(IAdminService admin)
		{
			_adminsrvice = admin;
		}
        public AdminIndexViewModel AdminIndex { get; set; }
        public void OnGet()
        {
			AdminIndex = _adminsrvice.GetIndexViewModel();

		}
    }
}
