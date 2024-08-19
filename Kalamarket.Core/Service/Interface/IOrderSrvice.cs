using Kalamarket.Core.DTOs.Order;
using Kalamarket.DataLayer.Eintitys.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalamarket.Core.Service.Interface
{
    public interface IOrderSrvice
    {
        int AddOrder(string username, int productId,int count);
        List<ShowCartViewModel> GetAllCartItems(string username);
        void RemoveItemFromCard(string username,int productId, int detailId);
        Discounts UseDiscounts(int orderId, string code);
        Order GetOrderById(int orderId);
        bool IsUserInOrder(int orderId,string username);
        void UpdateOrder(Order order);
        List<ShowMyOrdersViewModel> GetMyOrdersByUsername(string username);
        List<ShowMyOrderDetailViewModel> GetMyOrderDetailByUserName(string username,int orderId);
        int GetProductOrderCount(int productId);
        void AddUserOrder(int orderId, string username);
        List<ShowSellsViewModel> GetAllSells(string sellerUserName);
        UserOrderViewModel GetUserOrder(int UO_Id);
        List<OrderStatus> GetAllOrderStatus();
        void UpdateOrderStatus(int orderId,int statusId);
    }
}
