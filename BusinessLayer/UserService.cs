using DataAccessLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UserService : IUserService<User>
    {
        private readonly IUserRepository<User> _repository;
        public UserService(SqlConnection connection)
        {
            _repository = new UserDatabaseRepository(connection);

        }

        public void Add(User item)
        {
            _repository.Add(item);
        }
        public bool Get(string name, string username=null)
        {
           return _repository.Getting(name);
        }

        public bool validPassWord(string name, string password)
        {
           return _repository.validPass(name, password);
        }

        public void PasswordRestore(User item)
        {
            
          _repository.PasswordRestore(item);
        }
        public bool SavePhoto(int id, string destination)
        {
          return  _repository.SavePhoto(id, destination);
        }
        public void Logout()
        {
           _repository.Logout();
        }
        public bool LoadSession()
        {
            return _repository.LoadSession();
        }
        public int GetLastId()
        {
            return _repository.GetLastId();
        }

        public bool exist(string username, string mail)
        {
            return _repository.exist(username,mail);
        }

        public bool existMail(string username, string mail)
        {
            return _repository.existMail(username, mail);
        }
    }
}
