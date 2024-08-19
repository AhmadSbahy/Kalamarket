using Kalamarket.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Kalamarket.Web.ViewComponents
{
	public class ProductGroupComponent :ViewComponent
	{
	private readonly IProductService _productsrvice;
        public ProductGroupComponent(IProductService srvice)
        {
            _productsrvice = srvice;
        }
        public async Task<IViewComponentResult> InvokeAsync()
		{
			return await Task.FromResult((IViewComponentResult) View("ProductGroup",_productsrvice.GetAllGroups()));
		}
	}
}
