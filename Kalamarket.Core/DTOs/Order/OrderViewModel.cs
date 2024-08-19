using Kalamarket.DataLayer.Eintitys.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace Kalamarket.Core.DTOs.Order
{
	public class ShowCartViewModel
	{
		public int ProductId { get; set; }
		public int DetailId { get; set; }
		public int OrderId { get; set; }
		public string ProductImage { get; set; }
		public int Waranty { get; set; }
		public string SellerName { get; set; }
		public string ProductName { get; set; }
		public int ProductQountity { get; set; }
		public decimal DetailSum { get; set; }
		public decimal singlePrice { get; set; }
		public decimal FullPrice { get; set; }
		public Kalamarket.DataLayer.Eintitys.User.User User { get; set; }
	}

	public class UserOrderViewModel
	{
        public int UserOrderId { get; set; }
		public string FullName { get; set; }
		public string Price { get; set; }
		public DateTime CreateDate { get; set; }
		public string OrderStatus { get; set; }
		public AddressForUserOrderViewModel AddressForUserOrderViewModel { get; set; }
		public string UserPhoneNumber { get; set; }
	}

	public class AddressForUserOrderViewModel
	{
		public int AddressId { get; set; }
		public string RecipientName { get; set; }
		public string Landlinenumber { get; set; }
		public string State { get; set; }
		public string Plate { get; set; }
		public string City { get; set; }
		public string Floar { get; set; }
		public string PostalCode { get; set; }
		public string FullAddress { get; set; }
		public string? AddressDescripton { get; set; }
	}
	public class ShowSellsViewModel
	{
		public int UO_Id { get; set; }
		public string FullName { get; set; }
		public DateTime OrderDate { get; set; }
		public string Price { get; set; }
		public string OrderStatus { get; set; }
	}
	public enum Discounts
	{
		notfound, userused, finished, expire, success, notstarted
	}
	public class ShowMyOrdersViewModel
	{
		public int OrderId { get; set; }
		public DateTime CreateDate { get; set; }
		public bool IsFinaly { get; set; }
		public int OrderSum { get; set; }
		public string OrderStatus { get; set; }
	}
	public class ShowMyOrderDetailViewModel
	{
		public int detailId { get; set; }
		public string ProductName { get; set; }
		public decimal ProductPrice { get; set; }
		public int Count { get; set; }
		public int TotalPrice { get; set; }
	}
}
