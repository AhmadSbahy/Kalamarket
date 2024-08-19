using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.DTOs.User
{
	public class RegisterViewModle
    {
		[Display(Name = "نام كاربري")]
		[Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
		[MaxLength(100, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
		public string UserName { get; set; }
		[Display(Name = "ايميل")]
		[Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
		[MaxLength(200, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
		[EmailAddress(ErrorMessage = "ايميل وارد شده معتبر نمي باشد")]
		public string Email { get; set; }

		[Display(Name = "نام")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
		[RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "نام باید فقط شامل حروف فارسی باشد")]
		public string FullName { get; set; }

		[Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(20, ErrorMessage = "شماره تماس وارد شده معتبر نمي باشد")]
        public string PhoneNumber { get; set; }

        public int Code { get; set; }

		[Display(Name = "کلمه عبور")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(100, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
		[DataType(DataType.Password)]
		[RegularExpression(@"^.{4,}$", ErrorMessage = "رمز عبور باید حداقل 4 کاراکتر باشد")]
		public string Password { get; set; }


	}
	public class LoginViewModle
	{
		[Display(Name = "ايميل")]
		[Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
		[MaxLength(200, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
		[EmailAddress(ErrorMessage = "ايميل وارد شده معتبر نمي باشد")]
		public string Email { get; set; }
		[Display(Name = "كلمه عبور")]
		[Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
		[MaxLength(100, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
		[Display(Name = "مرا به خاطر بسپار")]
		[Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
		public bool RememberMe { get; set; }
    }
	public class ForgotPasswordViewModel
    {
		[Display(Name = "ايميل")]
		[Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
		[MaxLength(200, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
		[EmailAddress(ErrorMessage = "ايميل وارد شده معتبر نمي باشد")]
		public string Email { get; set; }
        public int Code { get; set; }
        public string? FullName { get; set; }
    }
	public class ResetPasswordViewModel
	{
        [Display(Name = "كلمه عبور")]
		[Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
		[MaxLength(100, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Display(Name = " تكرار كلمه عبور")]
		[Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
		[MaxLength(100, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
		[Compare("Password", ErrorMessage = "كلمه عبور با تكرار كلمه عبور مطابقت ندارد")]
		[DataType(DataType.Password)]
		public string RePassword { get; set; }
	}
}
