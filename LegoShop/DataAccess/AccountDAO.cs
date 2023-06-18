using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AccountDAO
    {
        private static AccountDAO instance = null;
        private static readonly object instanceLock = new object();
        legoShopContext _dbContext = new legoShopContext();
        public AccountDAO()
        {

        }
        public static AccountDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AccountDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Account> GetAccount()
        {
            try
            {
                var candate = _dbContext.Accounts.ToList();
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

        public Account Login(Account acc)
        {
            var cus = _dbContext.Accounts.FirstOrDefault(c => c.Email.Equals(acc.Email) && c.Password.Equals(acc.Password));
            if (cus != null)
            {
                return cus;
            }
            return null;
        }
        public void DeleteAccount(Account cate)
        {
            try
            {
                _dbContext.Accounts.Remove(cate);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public Account GetAccountById(int id)
        {
            try
            {
                var ca = _dbContext.Accounts.FirstOrDefault(x => x.AccountId == id);
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
        public void UpdateAccount(Account cate)
        {
            try
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Entry<Account>(cate).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void CreateNewAccount(Account cate)
        {
            try
            {
                var cus = _dbContext.Accounts.FirstOrDefault(c => c.Email.Equals(cate.Email));
                if (cus == null)
                {
                    _dbContext.Accounts.Add(cate);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public List<Account> SearchByKeyword(string keyword)
        {
            try
            {
                var x = _dbContext.Accounts.Where(x => x.Email.Contains(keyword)).ToList();
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
