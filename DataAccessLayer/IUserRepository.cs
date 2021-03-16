using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
   public interface IUserRepository<T>
    {
        bool Add(T item);
        bool Getting(string name, string username = null);
        bool validPass(string name, string password);
        bool SavePhoto(int id, string destination);
        int GetLastId();
        void CreateSession(int userId);
        List<T> Query(QueryPrameter[] parameters, string baseQuery);
        User GetUserSession();
        void DeleteUserSession(int userId);
        bool PasswordRestore(User user);
        T GetById(int id);
        bool LoadSession();
        void Logout();
        bool exist(string username, string mail);
        bool existMail(string username, string mail);


    }
}
