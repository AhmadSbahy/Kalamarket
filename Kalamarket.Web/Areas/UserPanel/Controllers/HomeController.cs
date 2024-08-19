using Kalamarket.Core.DTOs.UserPanel;
using Kalamarket.Core.Security;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Security.Cryptography;

namespace Kalamarket.Web.Areas.UserPanel.Controllers
{
	[Area("UserPanel")]
	[Authorize]
	public class HomeController : Controller
	{
		private IUserSrvice _usersrvice;
		private IOrderSrvice _ordersrvice;
		public HomeController(IUserSrvice userSrvice,IOrderSrvice order)
		{
			_usersrvice = userSrvice;
			_ordersrvice = order;
		}
		public IActionResult Index()
		{
			return View(_usersrvice.GetUserInformationByUserName(User.Identity.Name));
		}
		[Route("/UserPanel/Address/{id?}")]
		public IActionResult Address(int? id)
		{
			if (id != null)
			{
				if (_usersrvice.IsAddressForUser(User.Identity.Name, id))
				{
					var address = _usersrvice.GetAddressById(id);
					ViewBag.AddressId = id;
					return View(address);
				}
				else
				{
					return BadRequest();
				}
			}
			if (_usersrvice.IsUserHaveAddress(User.Identity.Name))
			{
				return RedirectToAction("ShowAddress");
			}
			return View();
		}
		[HttpPost]
		[Route("/UserPanel/Address/{id?}")]
		public IActionResult Address(Address address, int? id)
		{
			int userid = _usersrvice.GetUserIdByUserName(User.Identity.Name);

			address.UserId = userid;

			if (id == null)
			{
				_usersrvice.AddAddress(address);
			}
			else
			{
				address.AddressId = (int)id;
				_usersrvice.UpdateAddress(address);
			}
			return RedirectToAction("ShowAddress");
		}
		public IActionResult ShowAddress()
		{
			var ShowAddress = _usersrvice.GetShowAddressByUsername(User.Identity.Name);
			ViewBag.Id = ShowAddress.AddressId;
			return View(ShowAddress);
		}
		[Route("/UserPanel/EditInformation")]
		public IActionResult EditInformation()
		{
			return View();
		}
		[HttpPost]
		[Route("/UserPanel/EditInformation")]
		public IActionResult EditInformation(EditeUserInformationViewModel editeUser)
		{
			if (editeUser == null ||
	   editeUser.FullName == null &&
	   editeUser.Email == null &&
	   editeUser.OldPassword == null &&
	   editeUser.NewPassword == null &&
	   editeUser.PhoneNumber == null &&
	   editeUser.ReNewPassword == null)
			{
				// اگر همه‌ی ویژگی‌ها null باشند یا editUser خود null باشد، به Index ریدایرکت شود
				return RedirectToAction("Index");
			}
			var user = _usersrvice.GetUserByUserName(User.Identity.Name);
			if (editeUser.FullName != null)
			{
				user.FullName = editeUser.FullName;
			}
			if (editeUser.Email != null)
			{
				user.Email = editeUser.Email;
			}
			if (editeUser.OldPassword != null && editeUser.NewPassword != null && editeUser.ReNewPassword != null && !_usersrvice.IsPasswordExsitByUsername(User.Identity.Name, editeUser.OldPassword))
			{
				ModelState.AddModelError("OldPassword", "رمز عبور فعلی شما صحيح نمی باشد");

				if (editeUser.NewPassword == null)
				{
					ModelState.AddModelError("NewPassword", "براي تغيير رمز عبور خود بايد رمز عبور جديد را وارد كنيد");
					return View(editeUser);
				}
				if (editeUser.ReNewPassword == null)
				{
					ModelState.AddModelError("ReNewPassword", "براي تغيير رمز عبور خود بايد تكرار رمز عبور جديد را وارد كنيد");
					return View(editeUser);
				}
				return View(editeUser);
			}
			if (editeUser.PhoneNumber != null)
			{
				if (editeUser.PhoneNumber.Length > 11 || editeUser.PhoneNumber.Length < 11)
				{
					ModelState.AddModelError("PhoneNumber", "شماره تماس وارد شده معتبر نمی باشد");
					return View(editeUser);
				}
				else
				{
					user.PhoneNumber = editeUser.PhoneNumber;
				}
			}
			if (editeUser.OldPassword != null && editeUser.NewPassword != null && editeUser.ReNewPassword != null && _usersrvice.IsPasswordExsitByUsername(User.Identity.Name, editeUser.OldPassword))
			{
				user.Password = PasswordHelper.EncodePasswordMd5(editeUser.NewPassword);
			}

			_usersrvice.UpdateUser(user);



			return RedirectToAction("Index");
		}
		[Route("/UserPanel/Favorite")]
		public IActionResult Favorite()
		{
			return View(_usersrvice.GetFavoritesByUsername(User.Identity.Name));
		}
		[Route("/UserPanel/MyQoustions")]
		public IActionResult MyQoustions()
		{
			return View(_usersrvice.GetQoustionsByUsername(User.Identity.Name));
		}
		[Route("/UserPanel/MyOrders")]
		public IActionResult MyOrders()
		{
			return View(_ordersrvice.GetMyOrdersByUsername(User.Identity.Name));
		}
		[Route("/UserPanel/ShowOrder/{id}")]
		public IActionResult ShowOrder(int id)
		{
			return View(_ordersrvice.GetMyOrderDetailByUserName(User.Identity.Name,id));
		}
		[Route("/UserPanel/MySells")]
		public IActionResult MySells()
		{
			return View(_ordersrvice.GetAllSells(User.Identity.Name));
		}
		[Route("/UserPanel/GetOrderDetail/{id}")]
		public IActionResult GetOrderDetail(int id)
		{
			ViewBag.OrderStatus = _ordersrvice.GetAllOrderStatus();
			return View(_ordersrvice.GetUserOrder(id));
		}
        [HttpPost]
        public IActionResult ChangeOrderStatus(int orderId, int newStatusId)
        {
			_ordersrvice.UpdateOrderStatus(orderId,newStatusId);
            return Json(new { success = true, message = "وضعیت سفارش با موفقیت به روز رسانی شد." });
        }

    }
}
