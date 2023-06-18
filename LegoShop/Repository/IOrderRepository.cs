using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IOrderRepository
    {
        List<Order> GetOrder();
        List<Order> GetOrderByAccountId(int id);
        void DeleteOrder(Order cate);
        Order GetOrderById(int id);
        void UpdateOrder(Order cate);
        void CreateNewOrder(Order cate);
    }
}
