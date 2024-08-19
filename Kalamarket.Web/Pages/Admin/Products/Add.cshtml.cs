using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kalamarket.Web.Pages.Admin.Product
{
	[PermissionChecker(7)]
	public class AddModel : PageModel
    {
		private IAdminService _adminSrvice;
        private IUserSrvice _userSrvice;
		public AddModel(IAdminService admin,IUserSrvice user)
		{
			_adminSrvice = admin;
            _userSrvice = user;
		}
		[BindProperty]
        public Kalamarket.DataLayer.Eintitys.Product.Product Product { get; set; }
        public void OnGet()
        {
            var Groups = _adminSrvice.GetAllGroupForProduct();
            ViewData["Groups"] = new SelectList(Groups, "Value", "Text");
            
            var SubGroups = _adminSrvice.GetAllSubGroupForProduct(int.Parse(Groups.First().Value));
            ViewData["SubGroups"] = new SelectList(SubGroups, "Value", "Text");
        }
        public IActionResult OnPost(List<IFormFile> file)
        {
            if (!ModelState.IsValid)
            {
				var Groups = _adminSrvice.GetAllGroupForProduct();
				ViewData["Groups"] = new SelectList(Groups, "Value", "Text");

				var SubGroups = _adminSrvice.GetAllSubGroupForProduct(int.Parse(Groups.First().Value));
				ViewData["SubGroups"] = new SelectList(SubGroups, "Value", "Text");
				return Page();
			}

            Product.SellerId = _userSrvice.GetUserIdByUserName(User.Identity.Name);
            Product.SellCount = 0;
           int productid= _adminSrvice.AddProduct(Product);
            _adminSrvice.AddProductImage(file, productid);

            return RedirectToPage("Index");
        }
    }
}
