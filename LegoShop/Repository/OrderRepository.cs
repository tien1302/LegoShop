using BusinessObject.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void CreateNewOrder(Order cate) => OrderDAO.Instance.CreateNewOrder(cate);

        public void DeleteOrder(Order cate) => OrderDAO.Instance.DeleteOrder(cate);

        public Order GetOrderById(int id) => OrderDAO.Instance.GetOrderById(id);

        public List<Order> GetOrderByAccountId(int id) => OrderDAO.Instance.GetOrderByAccountId(id);
        public List<Order> GetOrder() => OrderDAO.Instance.GetOrder();

        public void UpdateOrder(Order cate) => OrderDAO.Instance.UpdateOder(cate);
    }
}
