using BusinessObject.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        public void CreateNewAccount(Account cate) => AccountDAO.Instance.CreateNewAccount(cate);

        public void DeleteAccount(Account cate) => AccountDAO.Instance.DeleteAccount(cate);

        public Account GetAccountById(int id) => AccountDAO.Instance.GetAccountById(id);

        public List<Account> GetAccount() => AccountDAO.Instance.GetAccount();

        public List<Account> SearchByKeyword(string keyword) => AccountDAO.Instance.SearchByKeyword(keyword);

        public void UpdateAccount(Account cate) => AccountDAO.Instance.UpdateAccount(cate);
        public Account Login(Account cus) => AccountDAO.Instance.Login(cus);
    }
}
