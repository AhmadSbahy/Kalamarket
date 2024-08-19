using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.ContactUs;
using Kalamarket.DataLayer.Eintitys.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kalamarket.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IAdminService _Adminsrvice;
        private readonly IProductService _productsrvice;
        private readonly IUserSrvice _usersrvice;
        private readonly IOrderSrvice _ordersrvice;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public ProductController(IAdminService admin, IProductService productsrvice, IUserSrvice userSrvice, IOrderSrvice ordersrvice, IHttpContextAccessor httpContextAccessor)
		{
			_Adminsrvice = admin;
			_productsrvice = productsrvice;
			_usersrvice = userSrvice;
			_ordersrvice = ordersrvice;
			_httpContextAccessor = httpContextAccessor;
		}
		public IActionResult Index(int pageId = 1, string filter = "", string getType = "All", int minPrice = 0, int maxPrice = 0, List<int> selectedGroups = null, int take = 0)
        {
            ViewBag.Groups = _productsrvice.GetAllGroups();
            ViewBag.PageId = pageId;
            ViewBag.selectG = selectedGroups;
            return View(_productsrvice.GetProducts(pageId, filter, getType, minPrice, maxPrice, selectedGroups, take));
        }
        [Authorize]
        [Route("AddFavorite/{id}")]
        public IActionResult AddFavorite(int id)
        {
            int UserId = _usersrvice.GetUserIdByUserName(User.Identity.Name);

            var Fav = new FavoriteProduct()
            {
                ProductId = id,
                UserId = UserId
            };
            if (_productsrvice.IsFavoriteInUser(UserId, id))
            {
                _productsrvice.RemoveFavorite(UserId, id);
            }
            else
            {
                _productsrvice.AddFavorite(Fav);
            }

            return Redirect($"/Details/{id}");
        }
        [Route("ContactUs")]
        [Authorize]
        public IActionResult ContactUs()
        {
            ViewBag.Subjects = _productsrvice.GetSubjects();
            return View();
        }
        [Authorize]
        [HttpPost]
        [Route("ContactUs")]
        public IActionResult ContactUs(ContactUs contact)
        {
            ViewBag.Subjects = _productsrvice.GetSubjects();
            if (contact.SubjectId == 0)
            {
                ModelState.AddModelError("SubjectId", "لطفا موضوعى را انتخاب كنيد");
                return View(contact);
            }
            if (contact.MassageTitle == null || contact.MassageTitle.Trim() == null)
            {
                ModelState.AddModelError("MassageTitle", "لطفا عنوان را وارد كنيد");
                return View(contact);
            }
            if (contact.Massagebody == null || contact.Massagebody.Trim() == null)
            {
                ModelState.AddModelError("Massagebody", "لطفا متن پیام را وارد كنيد");
                return View(contact);
            }
            var ContactUs = new ContactUs()
            {
                SubjectId = contact.SubjectId,
                UserId = _usersrvice.GetUserIdByUserName(User.Identity.Name),
                Massagebody = contact.Massagebody,
                MassageTitle = contact.MassageTitle
            };
            _productsrvice.AddContact(ContactUs);
            return Redirect("/");
        }
        [Authorize]
        [Route("BuyProduct/{id}")]
        public IActionResult BuyProduct(int id, string count)
        {
            int Count = int.Parse(count);
            _ordersrvice.AddOrder(User.Identity.Name, id, Count);

            return RedirectToAction("ShowCart", "Product");

        }
        [Authorize]
        public IActionResult ShowCart(bool? type)
        {
            var items = _ordersrvice.GetAllCartItems(User.Identity.Name);
            ViewBag.type = type;
            if (items != null && items.Count != 0 && items.Count >= 1)
            {
                return View(items);
            }
            else
            {
                return View("NoItems");
            }
        }
        [Authorize]
        [Route("romoveItem/{detailId}/{productId}")]
        public IActionResult romoveItem(int detailId, int productId)
        {
            _ordersrvice.RemoveItemFromCard(User.Identity.Name, productId, detailId);
            return RedirectToAction("ShowCart");
        }
        [Authorize]
        [Route("PayOrder/{id}")]
        public IActionResult PayOrder(int id)
        {
            var Order = _ordersrvice.GetOrderById(id);
            if(!_ordersrvice.IsUserInOrder(id,User.Identity.Name))
            {
                return null;
            }
            var payment = new ZarinpalSandbox.Payment((int)Order.OrderSum);
			string domain = _httpContextAccessor.HttpContext.Request.Host.Value;

			var res = payment.PaymentRequest("خريد", $"https://{domain}/OnlinePayment/" + Order.OrderId);
			
			if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            return null;
        }
    }
}
