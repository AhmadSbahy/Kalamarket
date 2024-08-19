using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.Order
{
    public class UserDiscounts
    {
        [Key]
        public int UD_ID { get; set; }
        public int UserId { get; set; }
        public int DiscountId { get; set; }

        public User.User User { get; set; }
        public Discount Discount { get; set; }
    }
}
