using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IProductRepository
    {
        List<Product> GetProduct();
        void DeleteProduct(Product cate);
        Product GetProductById(int id);
        void UpdateProduct(Product cate);
        void CreateNewProduct(Product cate);
        List<Product> SearchByKeyword(string keyword);
    }
}
