using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Kalamarket.Core.Convartor;
using Kalamarket.Core.DTOs.User;
using Kalamarket.Core.senders;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Eintitys.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Kalamarket.Web.Controllers
{
	public class AccountController : Controller
    {
        private readonly IUserSrvice _userSrvice;
		private readonly IViewRenderService _viewrender;
		private readonly IEmailSender _emailSender;

		public AccountController(IUserSrvice userSrvice, IViewRenderService viewrender, IEmailSender emailSender)
		{
			_userSrvice = userSrvice;
			_viewrender = viewrender;
			_emailSender = emailSender;
		}

		#region Rigister
		[Route("Register")]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost("Register")]
		public IActionResult Register(RegisterViewModle rigister)
		{
			if (!ModelState.IsValid)
			{
				return View(rigister);
			}

			if (_userSrvice.IsUserNameExsit(rigister.UserName))
			{
				ModelState.AddModelError("UserName", "نام كاربری معتبر نمی باشد");
				return View(rigister);
			}
			if (_userSrvice.IsEmailExsit(FIxedText.FixEmailText(rigister.Email)))
			{
				ModelState.AddModelError("Email", "ايميل وارد شده معتبر نمی باشد");
				return View(rigister);
			}
			if (rigister.PhoneNumber.Length > 11 || rigister.PhoneNumber.Length < 11)
			{
				ModelState.AddModelError("PhoneNumber", "شماره تماس وارد شده معتبر نمی باشد");
				return View(rigister);
			}
			Random random = new Random();

			int activationCode = random.Next(10000, 100000);

			rigister.Code = activationCode;

			_userSrvice.RegisterUser(rigister);

			#region Send Activation Email
			string body = _viewrender.RenderToStringAsync("_EmailSender", rigister);
			_emailSender.SendEmail(rigister.Email, "كد فعال سازی", body);
			#endregion
			ViewBag.Email = rigister.Email;
			return View("CheckCode", rigister);
		}
		#endregion

		#region CheckCode
		[HttpPost]
		public IActionResult CheckCode(string code, string email)
		{
			if (_userSrvice.IsCodeInUser(email, code))
			{
				return View("Success");
			}
			else
			{
				ViewBag.NotFound = true;
				ViewBag.Email = email;

				return View();
			}
		}
		#endregion

		#region Login
		[Route("Login")]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		[Route("Login")]
		public IActionResult Login(LoginViewModle login)
		{
			if (!ModelState.IsValid)
			{
				return View(login);
			}
			var User = _userSrvice.GetUserByLoginVM(login);
			if (User != null)
			{
				if (User.IsActive)
				{
					var claims = new List<Claim>
					{
						  new Claim(ClaimTypes.NameIdentifier, User.UserId.ToString()),
						  new Claim(ClaimTypes.Name, User.UserName)
                          //new Claim(ClaimTypes.GivenName,User.UserName)
                     };
					var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

					var principal = new ClaimsPrincipal(identity);

					var properties = new AuthenticationProperties
					{
						IsPersistent = login.RememberMe
					};

					HttpContext.SignInAsync(principal, properties);
					return Redirect("/");
				}
				else
				{
					ModelState.AddModelError("Email", "حساب كاربری شما فعال نشده است");
					return View(login);
				}
			}
			else
			{
				ModelState.AddModelError("Email", "اطلاعات وارد شده صحيح نمی باشد");
				return View(login);
			}
		}
		#endregion

		#region Logout Methode
		[Route("Logout")]
		public IActionResult Logout()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login");
		}
		#endregion

		#region ForgetPassword
		[Route("/ForgetPassword")]
		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		[Route("/ForgetPassword")]
		public IActionResult ForgetPassword(ForgotPasswordViewModel forgot)
		{
			if (!ModelState.IsValid)
				return View(forgot);
			if (forgot.Email == null || !_userSrvice.IsEmailExist(FIxedText.FixEmailText(forgot.Email)))
			{
				ModelState.AddModelError("Email", "ايميل وارد شده معتبر نمی باشد");
				return View(forgot);
			}
			var user = _userSrvice.GetUserForResetPass(FIxedText.FixEmailText(forgot.Email));
			string bodyEmail = _viewrender.RenderToStringAsync("_ForgotPass", user);
			_emailSender.SendEmail(user.Email.Trim(), "بازيابی حساب كاربری", bodyEmail);
			ViewBag.IsForgot = true;
			ViewBag.Email = user.Email.Trim();
			return View("CheckCodeForResetPass", user);
		}
		[HttpPost]
		public IActionResult CheckCodeForResetPass(string code,string email) 
		{
			
			ViewBag.Email = email;
			if (_userSrvice.IsCodeInUser(email, code))
			{
				ViewBag.Email = email;
				return View("ResetPass");
			}
			else
			{
				ViewBag.NotFound = true;
				ViewBag.Email = email;

				return View();
			}
		}
		[HttpPost]
		public IActionResult ResetPass(ResetPasswordViewModel reset,string? email)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Email = email;
				return View(reset);
			}

			_userSrvice.UpdateUserPassByEmail(email : email, password: reset.Password);

		
			return RedirectToAction("Login");
		}
		#endregion

	}
}
