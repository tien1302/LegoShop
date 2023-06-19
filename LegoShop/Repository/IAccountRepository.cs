using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IAccountRepository
    {
        List<Account> GetAccount();
        void DeleteAccount(Account cate);
        Account GetAccountById(int id);
        void UpdateAccount(Account cate);
        void CreateNewAccount(Account cate);
        List<Account> SearchByKeyword(string keyword);
        Account Login(Account cus);
    }
}
