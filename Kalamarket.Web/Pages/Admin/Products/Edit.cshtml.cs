using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kalamarket.Web.Pages.Admin.Product
{
	[PermissionChecker(8)]
	public class EditModel : PageModel
    {
		private IAdminService _adminSrvice;
		private IUserSrvice _userSrvice;
		public EditModel(IAdminService admin, IUserSrvice user)
		{
			_adminSrvice = admin;
			_userSrvice = user;
		}
		[BindProperty]
		public Kalamarket.DataLayer.Eintitys.Product.Product Product { get; set; }
		public void OnGet(int id)
		{
			Product = _adminSrvice.GetProductById(id);
			Product.Images = _adminSrvice.GetProducImagetName(id);
			//var Groups = _adminSrvice.GetAllGroupForProduct();
			//ViewData["Groups"] = new SelectList(Groups, "Value", "Text");

			//var SubGroups = _adminSrvice.GetAllSubGroupForProduct(int.Parse(Groups.First().Value));
			//ViewData["SubGroups"] = new SelectList(SubGroups, "Value", "Text");
			var groups = _adminSrvice.GetAllGroupForProduct();
			ViewData["Groups"] = new SelectList(groups, "Value", "Text", Product.GroupId);

			List<SelectListItem> subgrups = new List<SelectListItem>()
			{
				new SelectListItem(){Text="انتخاب كنيد",Value=""}
			};
			subgrups.AddRange(_adminSrvice.GetAllSubGroupForProduct(Product.GroupId));
			string selectedsubgroup = null;
			if (Product.SubGroup != null)
			{
				selectedsubgroup = Product.SubGroup.ToString();
			}
			ViewData["SubGroups"] = new SelectList(subgrups, "Value", "Text", selectedsubgroup);
		}
		public IActionResult OnPost(int id,List<IFormFile>? file)
		{			
			if (file.Count >= 1 || file.Count != 0)
			{
				_adminSrvice.DeleteProductImage(id);
				_adminSrvice.AddProductImage(file, id);
			}


			_adminSrvice.UpdateProduct(Product);

			return RedirectToPage("Index");
		}
	}
}
