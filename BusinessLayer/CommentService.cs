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
    public class CommentService : IService<Comment>
    {
        private readonly IRepository<Comment> _repository;
        private readonly CommentDatabaseRepository _innerRepository;
        public CommentService(SqlConnection connection)
        {
            _repository = new CommentDatabaseRepository(connection);
            _innerRepository = new CommentDatabaseRepository(connection);
        }

        public void Add(Comment item)
        {
            _repository.Add(item);
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

        public void Update(Comment item)
        {
            _repository.Update(item);
        }
        public int GetbyId(string username)
        {
            return _innerRepository.GetbyId(username);
        }
        public DataSet ListDataFriends()
        {
            return _innerRepository.ListDataFriends();
        }
        public int GetbyIdFriends()
        {
            return _innerRepository.GetbyIdFriends();
        }
    }
}