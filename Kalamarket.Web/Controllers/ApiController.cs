using Kalamarket.Core.Convartor;
using Kalamarket.Core.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kalamarket.Web.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ApiController : ControllerBase
	{
		private readonly IAdminService _Adminsrvice;
		private readonly IProductService _productsrvice;
		private readonly IUserSrvice _usersrvice;
		private readonly IOrderSrvice _ordersrvice;
		public ApiController(IAdminService admin, IProductService productsrvice, IUserSrvice userSrvice, IOrderSrvice order)
		{
			_Adminsrvice = admin;
			_productsrvice = productsrvice;
			_usersrvice = userSrvice;
			_ordersrvice = order;

		}

		[HttpGet]
		public IActionResult GetSlider()
		{
			return JsonResponseStatus.Ok(_productsrvice.GetBanners());
		}
	}
}
