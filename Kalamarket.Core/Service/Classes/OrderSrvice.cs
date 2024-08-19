using Kalamarket.Core.DTOs.Order;
using Kalamarket.Core.Service.Interface;
using Kalamarket.DataLayer.Context;
using Kalamarket.DataLayer.Eintitys.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.Service.Classes
{
    public class OrderSrvice : IOrderSrvice
    {
        private KalamarketContext _context;
        private IUserSrvice _userSrvice;
        private IProductService _productSrvice;

        public OrderSrvice(KalamarketContext context, IUserSrvice user, IProductService productSrvice)
        {
            _context = context;
            _userSrvice = user;
            _productSrvice = productSrvice;
        }
        public int AddOrder(string username, int productId, int count)
         {
			int userId = _userSrvice.GetUserIdByUserName(username);

			var order = _context.Orders.SingleOrDefault(o => o.UserId == userId && !o.IsFinaly);

			var product = _productSrvice.GetProductById(productId);
			double price;

			if (product.ProductDiscounts != null)
			{
			
				price = (double)product.ProductPrice * (1 - (double)product.ProductDiscounts.DiscountPercent / 100);
			}
			else
			{
				price = (double)product.ProductPrice;
			}

			if (order == null)
			{
				order = new DataLayer.Eintitys.Order.Order()
				{
					IsFinaly = false,
					CreateDate = DateTime.Now,
					UserId = userId,
					OrderSum = (decimal)price * count,
					OrderDetails = new List<DataLayer.Eintitys.Order.OrderDetail>()
					{
						new DataLayer.Eintitys.Order.OrderDetail()
						{
							Count = count,
							Price = (decimal)price * count,
							ProductId = productId
						}
					}
				};
				_context.Orders.Add(order);
			}
			else
			{
				var detail = _context.OrderDetails
					.SingleOrDefault(d => d.ProductId == productId && d.OrderId == order.OrderId);
				if (detail == null)
				{
					detail = new DataLayer.Eintitys.Order.OrderDetail()
					{
						Count = count,
						OrderId = order.OrderId,
						Price = (decimal)price * count,
						ProductId = productId
					};
					order.OrderSum = order.OrderSum + (decimal)price * count;
					_context.Orders.Update(order);
					_context.OrderDetails.Add(detail);
				}
				else
				{
					detail.Count += count;
					detail.Price += (decimal)price * count;
					order.OrderSum += (decimal)price * count;

					_context.Orders.Update(order);
					_context.OrderDetails.Update(detail);
				}
			}
			_context.SaveChanges();

			return order.OrderId;
		}

        public List<ShowCartViewModel> GetAllCartItems(string username)
        {
            int userId = _userSrvice.GetUserIdByUserName(username);

            var order = _context.Orders.SingleOrDefault(o => o.UserId == userId && !o.IsFinaly);
            if (order != null)
            {
                return _context.OrderDetails.Include(p => p.Order)
                    .ThenInclude(p=> p.User)
                    .ThenInclude(p=> p.Address)
                    .Include(p => p.Product)
                    .ThenInclude(p => p.Images)
                    .Where(p => p.OrderId == order.OrderId && p.Order.UserId == userId)
                    .Select(p => new ShowCartViewModel()
                    {
                        ProductId = p.ProductId,
                        DetailSum = p.Price,
                        ProductImage = p.Product.Images[0].ImageName,
                        ProductName = p.Product.ProductName,
                        ProductQountity = p.Count,
                        SellerName = p.Order.User.FullName,
                        Waranty = (int)p.Product.WarrantyMonthDate,
                        singlePrice = p.Product.ProductPrice,
                        FullPrice = p.Order.OrderSum,
                        DetailId = p.DetailId,
                        OrderId = p.OrderId,
                        User = p.Order.User
                    }).ToList();
            }
            else
            {
                return null;
            }
        }

		public List<ShowMyOrderDetailViewModel> GetMyOrderDetailByUserName(string username,int orderId)
		{
			int userId = _userSrvice.GetUserIdByUserName(username);
            return _context.OrderDetails.Include(p=> p.Product).Where(p => p.OrderId == orderId &&p.Order.UserId == userId)
                .Select(p => new ShowMyOrderDetailViewModel()
                {
                    Count = p.Count,
                    detailId = p.DetailId,
                    ProductName = p.Product.ProductName,
                    ProductPrice = p.Price,
                    TotalPrice = (int)p.Order.OrderSum
                }).ToList();

		}

		public List<ShowMyOrdersViewModel> GetMyOrdersByUsername(string username)
        {
            int userId = _userSrvice.GetUserIdByUserName(username);
            return _context.Orders
	            .Include(x=>x.UserOrder)
	            .ThenInclude(x=>x.Status)
	            .Where(p => p.UserId == userId)
                .Select(p => new ShowMyOrdersViewModel()
                {
                    CreateDate = p.CreateDate,
                    IsFinaly = p.IsFinaly,
                    OrderSum = (int)p.OrderSum,
                    OrderId = p.OrderId,
                    OrderStatus = p.UserOrder.Status.StatusTitle
                }).ToList();

        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders
                .Include(p=> p.OrderDetails)
                .ThenInclude(p=> p.Product)
                .SingleOrDefault(p=> p.OrderId == orderId);
        }

		public int GetProductOrderCount(int productId)
		{
            return _context.OrderDetails.Include(p=> p.Order).Count(p => p.ProductId == productId && p.Order.IsFinaly);
		}

		public void AddUserOrder(int orderId, string username)
		{
			int userId = _userSrvice.GetUserIdByUserName(username);

			UserOrder userOrder = new UserOrder()
			{
                UserId = userId,
                GetOrderDate = DateTime.Now,
                OrderId = orderId,
                OrderStatusId = 1,
			};
			_context.UserOrders.Add(userOrder);
			_context.SaveChanges();
		}

		public bool IsUserInOrder(int orderId,string username)
        {
            int userId = _userSrvice.GetUserIdByUserName(username);

            return _context.Orders.Any(p => p.UserId == userId && p.OrderId == orderId && !p.IsFinaly);
        }

        public void RemoveItemFromCard(string username, int productId, int detailId)
        {
            int userId = _userSrvice.GetUserIdByUserName(username);

            var orderde = _context.OrderDetails.Include(p => p.Order)
                  .Where(p => p.Order.UserId == userId && p.DetailId == detailId && !p.Order.IsFinaly);

            if (orderde != null)
            {
                _context.OrderDetails.Remove(orderde.Where(p => p.ProductId == productId).Single());
                var order = _context.Orders.Where(p => p.UserId == userId && !p.IsFinaly).SingleOrDefault();
                order.OrderSum -= orderde.Where(p => p.ProductId == productId).Single().Price;
                if (order.OrderSum <= 0)
                {
                    order.OrderSum = 0;
                }
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public Discounts UseDiscounts(int orderId, string code)
        {
            var discount = _context.Discounts.SingleOrDefault(d => d.DiscountCode == code);
            if (discount == null)
            {
                return Discounts.notfound;
            }
            if (discount.StartDate != null && discount.StartDate > DateTime.Now)
            {
                return Discounts.notstarted;
            }
            if (discount.EndDate != null && discount.EndDate < DateTime.Now)
            {
                return Discounts.expire;
            }
            if(discount.UsableCount != null && discount.UsableCount < 1)
            {
                return Discounts.finished;
            }
            var order = _context.Orders.Find(orderId);
            if(_context.UserDiscounts.Any(p=> p.DiscountId == discount.DiscountId))
            {
                return Discounts.userused;
            }
            decimal pearsent = (order.OrderSum * discount.DiscountPersent) / 100;
            order.OrderSum = order.OrderSum - (int)pearsent;

            _context.Orders.Update(order);
            if (discount.UsableCount != null)
            {
                discount.UsableCount -= 1;
            }
            _context.Discounts.Update(discount);

            _context.UserDiscounts.Add(new UserDiscounts()
            {
                UserId = order.UserId,
                DiscountId = discount.DiscountId
            });
            _context.SaveChanges();
            return Discounts.success;
        }

		public List<ShowSellsViewModel> GetAllSells(string sellerUserName)
		{
			int userId = _userSrvice.GetUserIdByUserName(sellerUserName);

            var value = _context.UserOrders
	            .Include(x => x.User)
	            .Include(x => x.Order)
	             .Select(p => new ShowSellsViewModel()
	            {
		            Price = p.Order.OrderSum.ToString("#,0 تومان"),
		            FullName = p.User.FullName,
		            OrderDate = p.Order.CreateDate,
                    UO_Id = p.UserOrderId,
                    OrderStatus = _context.OrderStatus.Single(x=>x.OrderStatusId == p.OrderStatusId).StatusTitle
	            }).ToList();

            return value;
		}

		public UserOrderViewModel GetUserOrder(int UO_Id)
		{
			// بارگذاری تمامی وضعیت‌ها در یک دیکشنری
			var orderStatuses = _context.OrderStatus.ToDictionary(os => os.OrderStatusId, os => os.StatusTitle);

			return _context.UserOrders
				.Include(x => x.Order)
				.Include(x => x.User)
				.ThenInclude(x => x.Address)
				.Where(x => x.UserOrderId == UO_Id)
				.Select(p => new UserOrderViewModel()
				{
					Price = p.Order.OrderSum.ToString("#,0 تومان"),
					FullName = p.User.FullName,
					CreateDate = p.GetOrderDate,
					OrderStatus = orderStatuses[p.OrderStatusId],
					UserPhoneNumber = p.User.PhoneNumber,
                    UserOrderId = p.UserOrderId,
					AddressForUserOrderViewModel = new AddressForUserOrderViewModel()
					{
						City = p.User.Address.City,
						AddressDescripton = p.User.Address.AddressDescripton,
						Floar = p.User.Address.Floar,
						AddressId = p.User.Address.AddressId,
						FullAddress = p.User.Address.FullAddress,
						Landlinenumber = p.User.Address.Landlinenumber,
						Plate = p.User.Address.Plate,
						PostalCode = p.User.Address.PostalCode,
						RecipientName = p.User.Address.RecipientName,
						State = p.User.Address.State
					}
				}).First();
		}

		public List<OrderStatus> GetAllOrderStatus()
		{
			return _context.OrderStatus.ToList();
		}

        public void UpdateOrderStatus(int orderId, int statusId)
        {
            var UserOrder = _context.UserOrders.Find(orderId);
            UserOrder.OrderStatusId = statusId;
            _context.UserOrders.Update(UserOrder);
            _context.SaveChanges();
        }
    }
}
