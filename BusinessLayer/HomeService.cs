using DataAccessLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public  class HomeService : IService<Home>
    {
        private readonly IRepository<Home> _repository;
        private readonly HomeDatabaseRepository _innerrepository;
        public HomeService(SqlConnection connection)
        {
            _repository = new HomeDatabaseRepository(connection);
            _innerrepository = new HomeDatabaseRepository(connection);

        }
      public void Add(Home item)
        {
            _repository.Add(item);
        }
        public void AddPhoto(Home item)
        {
            _innerrepository.AddPhoto(item);
        }
        public void Delete(int index)
        {
            _repository.Delete(index);
        }
        public DataSet GetAll(string username)
        {
            return _repository.List(username);
        }
     
        public DataTable GetAllData(string username)
        {
            return _repository.ListData(username);
        }
        public DataTable GetAllDataPhoto(string username)
        {
            return _innerrepository.ListDataPhoto(username);
        }
        public void Update(Home item)
        {
            _repository.Update(item);
        }
        public string getUserName()
        {
           return _innerrepository.getUserName();
        }
        public bool SavePhoto(int id, string destination)
        {
            return _innerrepository.SavePhoto(id, destination);
        }
        public int GetLastId()
        {
            return _innerrepository.GetLastId();
        }
        }
}
