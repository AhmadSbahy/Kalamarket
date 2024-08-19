using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.Order
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }
        [Required(ErrorMessage ="كد اجبارى مى باشد")]
        public string DiscountCode { get; set; }
		[Required(ErrorMessage = "درصد تخفيف اجبارى مى باشد")]
		public int DiscountPersent { get; set; }
        public int? UsableCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<UserDiscounts>? UserDiscounts { get; set; }
    }
}
