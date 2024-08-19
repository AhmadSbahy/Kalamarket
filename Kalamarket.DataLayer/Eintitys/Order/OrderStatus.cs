using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.Order
{
	public class OrderStatus
	{
		[Key]
		public int OrderStatusId { get; set; }
		[Required]
		[MaxLength(150)]
		public string StatusTitle { get; set; }

		public List<UserOrder> UserOrders { get; set; }
	}
}
