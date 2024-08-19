using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Order;

namespace Kalamarket.Web.Pages.Admin.OrderStatus
{
    public class IndexModel : PageModel
    {
        private readonly Kalamarket.DataLayer.Context.KalamarketContext _context;

        public IndexModel(Kalamarket.DataLayer.Context.KalamarketContext context)
        {
            _context = context;
        }

        public IList<DataLayer.Eintitys.Order.OrderStatus> OrderStatus { get;set; }

        public async Task OnGetAsync()
        {
            OrderStatus = await _context.OrderStatus.ToListAsync();
        }
    }
}
