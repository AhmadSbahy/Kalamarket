using Kalamarket.DataLayer.Eintitys.Groups;
using Kalamarket.DataLayer.Eintitys.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.Product
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }


        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(500, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
        public string ProductName { get; set; }

        [Display(Name = "توضيح")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        public string ProductDescription { get; set; }

        [Display(Name = "نام گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        public int GroupId { get; set; }
		public int? SubGroup { get; set; }
		[Display(Name = "تعداد فروش")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        public int SellCount { get; set; }
        [Display(Name = "تگ ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        public string Tags { get; set; }
        [Display(Name = "ویژگی ‌ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(800, ErrorMessage = "{0} نميتواند بيشتر از {1} كاراكتر باشد")]
        public string ProductProperty { get; set; }
        [Display(Name = "نام فروشنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        public int SellerId { get; set; }
        [Display(Name = "گارانتی")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        public int? WarrantyMonthDate { get; set; }
        [Display(Name = "قيمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        public decimal ProductPrice { get; set; }
        [Display(Name = "تعداد در انبار")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        public int QuantityStock { get; set; }
        [Display(Name = "نقاط قوت")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(900)]
        public string? Goodpoints { get; set; }
        [Display(Name = "نقاط ضعف")]
        [Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
        [MaxLength(900)]
        public string? Badpoints { get; set; }
        [Display(Name = "حذف شده ؟")]
        public bool IsDeleted { get; set; }

        [ForeignKey("GroupId")]
        public Group? Group { get; set; }

        public List<ProductImage>? Images { get; set; }
        [ForeignKey("SellerId")]
        public User.User? User { get; set; }

        public List<ProductComment>? ProductComments { get; set; }
        public List<ProductQuestions.ProductQuestions>? ProductQuestions { get; set; }
        public List<FavoriteProduct>? FavoriteProducts { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
        public ProductDiscount? ProductDiscounts { get; set; }
	}
}
