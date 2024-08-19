using Kalamarket.Core.Service.Classes;
using Kalamarket.Core.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kalamarket.Web.Areas.UserPanel.Controllers
{
	[Area("UserPanel")]
	[Authorize]
	public class MyProductController : Controller
	{
		private readonly IProductService _productService;
		private readonly IAdminService _adminSrvice;

		public MyProductController(IProductService productService, IAdminService adminSrvice)
		{
			_productService = productService;
			_adminSrvice = adminSrvice;
		}
		[Route("UserPanel/MyProducts")]
		public IActionResult Index()
		{
			return View(_productService.GetMyProducts(User.Identity.Name));
		}
		[Route("UserPanel/DeleteMyProducts/{id}")]
		public IActionResult DeleteProduct(int id)
		{
			_adminSrvice.DeleteProductImage(id);
			_adminSrvice.DeletProduct(id);
			return RedirectToAction("Index");
		}
	}
}
