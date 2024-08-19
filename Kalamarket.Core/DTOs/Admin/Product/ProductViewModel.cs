using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kalamarket.DataLayer.Eintitys.Product;

namespace Kalamarket.Core.DTOs.Admin.Product
{
    public class ShowProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string SellerName { get; set; }
        public int SellCount { get; set; }
        public decimal ProductPrice { get; set; }
        public ProductDiscount ProductDiscount { get; set; }
    }
    public class DeleteProductViewModel
    {
        public int Id { get; set; }
        public string Group { get; set; }
        public string Sub { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

    }
}
