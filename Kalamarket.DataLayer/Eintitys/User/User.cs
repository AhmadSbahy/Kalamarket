using Kalamarket.DataLayer.Eintitys.Blog;
using Kalamarket.DataLayer.Eintitys.Order;
using Kalamarket.DataLayer.Eintitys.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.User
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Display(Name = "نام كاربري")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(200, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
        public string UserName { get; set; }
        [Display(Name = "ايميل")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(200, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
        [EmailAddress(ErrorMessage = "ايميل وارد شده معتبر نمي باشد")]

        public string Email { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(200, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
        public string FullName { get; set; }
        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(20, ErrorMessage = "شماره تماس وارد شده معتبر نمي باشد")]
        public string PhoneNumber { get; set; }

        [Display(Name = "كلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(100, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "كد فعال سازي")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        public int ActivCode { get; set; }
        [Display(Name = "غعال شده")]
        public bool IsActive { get; set; }
        public DateTime RigisterDate { get; set; }
        [Display(Name = "حذف شده")]
        public bool IsDelete { get; set; }


        public Address.Address Address { get; set; }
        public List<Product.Product> Products { get; set; }
        public List<Product.ProductComment> ProductComments { get; set; }
        public List<ProductQuestions.ProductQuestions> ProductQuestions { get; set; }
		public List<FavoriteProduct>? FavoriteProducts { get; set; }
        public List<ContactUs.ContactUs> ContactUs { get; set; }
        public List<Order.Order> Orders { get; set; }
        public List<UserDiscounts> UserDiscounts { get; set; }
        public List<Blog.Blog> Blogs{ get; set; }
        public List<BlogComment> BlogComments { get; set; }
		public List<UserRole>? UserRoles { get; set; }
		public List<UserOrder> UserOrders { get; set; }
	}
}
