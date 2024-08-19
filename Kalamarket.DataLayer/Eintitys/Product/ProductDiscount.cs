using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.Product
{
	public class ProductDiscount
	{
		[Key]
		public int DiscountId { get; set; }

		[Display(Name = "درصد تخفیف")]
		[Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
		public int DiscountPercent { get; set; }

		[Display(Name = "تاریخ شروع")]
		[Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
		public DateTime StartDate { get; set; }

		[Display(Name = "تاریخ پایان")]
		[Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
		public DateTime EndDate { get; set; }

		public int ProductId { get; set; }
		[ForeignKey("ProductId")]
		public Product? Product { get; set; }
	}
}
