using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        legoShopContext _dbContext = new legoShopContext();
        public ProductDAO()
        {

        }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Product> GetProduct()
        {
            try
            {
                var candate = _dbContext.Products.ToList();
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
        public void DeleteProduct(Product cate)
        {
            try
            {
                _dbContext.Products.Remove(cate);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public Product GetProductById(int id)
        {
            try
            {
                var ca = _dbContext.Products.FirstOrDefault(x => x.ProductId == id);
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
        public void UpdateProduct(Product cate)
        {
            try
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Entry<Product>(cate).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void CreateNewProduct(Product pro)
        {
            try
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Products.Add(pro);
                _dbContext.SaveChanges();
                _dbContext.Entry(pro).State = EntityState.Detached;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public List<Product> SearchByKeyword(string keyword)
        {
            try
            {
                var x = _dbContext.Products.Where(x => x.Name.Contains(keyword)).ToList();
                if (x != null)
                {
                    return x;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
