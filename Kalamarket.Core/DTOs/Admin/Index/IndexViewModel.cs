using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.DTOs.Admin.Index
{
    public class AdminIndexViewModel
    {
        public decimal TotalRevenue { get; set; }
        public int TotalCustomer { get; set; }
        public int TotalOrders { get; set; }
    }
}
