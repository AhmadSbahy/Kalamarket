using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.DataLayer.Eintitys.Order
{
	public class UserOrder
	{
		[Key]
		public int UserOrderId { get; set; }
		[Required]
		public int UserId { get; set; }
		[Required]
		public int OrderId { get; set; }
		[Required]
		public int OrderStatusId { get; set; }

		public DateTime GetOrderDate { get; set; }

		[ForeignKey("UserId")]
		public User.User User { get; set; }
		[ForeignKey("OrderId")]
		public Order Order { get; set; }
		[ForeignKey("OrderStatusId")]
		public OrderStatus Status { get; set; }
	}
}
