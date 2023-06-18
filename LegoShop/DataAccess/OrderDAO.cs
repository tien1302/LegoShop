using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO? instance = null;
        private static readonly object instanceLock = new object();
        legoShopContext _dbContext = new legoShopContext();
        public OrderDAO()
        {

        }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Order> GetOrder()
        {
            try
            {
                var candate = _dbContext.Orders.ToList();
                if (candate != null)
                {
                    return candate;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void DeleteOrder(Order cate)
        {
            try
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Orders.Remove(cate);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public Order GetOrderById(int id)
        {
            try
            {
                var ca = _dbContext.Orders.FirstOrDefault(x => x.OrderId == id);
                if (ca != null)
                {
                    return ca;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<Order> GetOrderByAccountId(int id)
        {
            try
            {
                var ca = _dbContext.Orders.Where(x => x.AccountId == id).ToList();
                if (ca != null)
                {
                    return ca;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void UpdateOder(Order cate)
        {
            try
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Entry<Order>(cate).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void CreateNewOrder(Order pro)
        {
            try
            {
                _dbContext.ChangeTracker.Clear();
                pro.OrderDate = DateTime.Now;
                pro.OrderStatus = "Pending";
                _dbContext.Orders.Add(pro);
                _dbContext.SaveChanges();
                _dbContext.Entry(pro).State = EntityState.Detached;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
