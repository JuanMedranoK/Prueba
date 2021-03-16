using DataAccessLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IUserService<T>
    {
        void Add(T item);
        bool Get(string name, string username = null);
        bool validPassWord(string name, string password);
        bool SavePhoto(int id, string destination);
        int GetLastId();
        void PasswordRestore(User user);
        bool LoadSession();
        void Logout();
        bool exist(string username, string mail);
        bool existMail(string username, string mail);



    }
}