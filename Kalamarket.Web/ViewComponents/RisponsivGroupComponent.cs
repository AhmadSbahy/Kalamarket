using Kalamarket.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Kalamarket.Web.ViewComponents
{
	public class RisponsivGroupComponent : ViewComponent
	{

		private readonly IProductService _productsrvice;
		public RisponsivGroupComponent(IProductService srvice)
		{
			_productsrvice = srvice;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return await Task.FromResult((IViewComponentResult)View("RespnsivGroupComponent", _productsrvice.GetAllGroups()));
		}
	}
}
