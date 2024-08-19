using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kalamarket.DataLayer.Eintitys.Product;

namespace Kalamarket.Core.DTOs.Product
{
    public class ProductCommentViewModel
    {
        public int CommentId { get; set; }
        public bool IsRecomended { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string GoodPoints { get; set; }
        public string BadPoints { get; set; }
        public DateTime  CreateDate { get; set; }
    }
    
    public class QoustionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public bool IsAnswerd { get; set; }
    }

    public class AnserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
    }

    public class ShowProductListViewModel
    {
        public int ProductId { get; set; }
        public string ImageName { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public ProductDiscount ProductDiscount { get; set; }

    }

    public class MyProductsViewModel
    {
	    public int ProductId { get; set; }
	    public string ImageName { get; set; }
	    public decimal Price { get; set; }
	    public string ProductName { get; set; }
    }
}
