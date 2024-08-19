using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kalamarket.DataLayer.Eintitys.Product;

namespace Kalamarket.Core.DTOs.Index
{
	public class ProductBoxViewModel
	{
        public int Id { get; set; }
        public string FirstImage { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public int GroupId { get; set; }
        public int ProductId { get; set; }
        public ProductDiscount ProductDiscount { get; set; }

    }
    public class IndexGroupsViewModel
    {
        public string GroupName { get; set; }
        public List<ProductBoxViewModel> Products { get; set; }
    }
    public class IndexGroupSearch
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
    public class IndexOrderViewModel
    {
        public int Price { get; set; }
        public int Count { get; set; }
    }
}
