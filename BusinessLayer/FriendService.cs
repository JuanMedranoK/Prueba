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
    public class FriendService : IService<Friend>
    {
        private readonly IRepository<Friend> _repository;
        private readonly FriendDatabaseRepository _innerRepository;
        public FriendService(SqlConnection connection)
        {
            _repository = new FriendDatabaseRepository(connection);
            _innerRepository = new FriendDatabaseRepository(connection);
        }

        public void Add(Friend item)
        {
            _repository.Add(item);
        }

        public void Delete(int index)
        {
            _repository.Delete(index);
        }

        public bool DeleteFriend(string username, string FriendUserName)
        {
            return _innerRepository.DeleteFriend(username, FriendUserName);
        }


        public DataSet GetAll(string userId)
        {
            return _repository.List(userId);
        }

        public bool exist(string userId, string friendId)
        {
            return _innerRepository.exist(userId, friendId);
        }

        public int GetbyId(string username)
        {
            return _innerRepository.GetbyId(username);
        }

        public void Update(Friend item)
        {
            _repository.Update(item);
        }

        public DataSet GetPostList(string username)
        {
            return _innerRepository.PostList(username);
        }
        public DataTable ListDataPhoto()
        {
            return _innerRepository.ListDataPhoto();
        }
        public DataSet PostList(string username)
        {
            return _innerRepository.PostList(username);
        }

        public int GetbyPostId(string username)
        {
            return _innerRepository.GetbyPostId(username);
        }

        public DataTable GetAllData(string username)
        {
            return _repository.ListData(username);
        }
     
    }
}