using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class EventService : IService<Event>
    {
        private readonly IRepository<Event> _repository;
        private readonly EventDatabaseRepository _innerRepository;
        public EventService(SqlConnection connection)
        {
            _repository = new EventDatabaseRepository(connection);
            _innerRepository = new EventDatabaseRepository(connection);
        }

        public void Add(Event item)
        {
            _repository.Add(item);
        }

        public void Delete(int index)
        {
            _repository.Delete(index);
        }
        public void DeleteFromEvent(string username, int eventId)
        {
            _innerRepository.DeleteFromEvent(username, eventId);
        }
    

        public DataSet GetAll(string username)
        {
           return _repository.List(username);
        }
        public DataSet GetFriendsList(int eventId)
        {
            return _innerRepository.GetFriendsList(eventId);
        }
        public DataSet GetFriendEventList(string username)
        {
            return _innerRepository.GetFriendEventList(username);
        }
        public int GetbyId(string username)
        {
           return _innerRepository.GetbyId(username);
        }
        public int GetEventsFriendsCountById(string username)
        {
            return _innerRepository.GetEventsFriendsCountById(username);
        }

        public void Update(Event item)
        {
            _repository.Update(item);
        }
     
        public void AddEvenInvitation(Event item)
        {
            _innerRepository.AddEvenInvitation(item);
        }
        public int  GetfriendsbyId(int eventId)
        {
            return _innerRepository.GetfriendsbyId(eventId);
        }
        public int GetlastId()
        {
            return _innerRepository.GetLastId();
        }
        public int GetInvitations(int eventId)
        {
            return _innerRepository.GetInvitations(eventId);
        }
        public void UpdateInvitation(int accept, int eventId)
        {
            _innerRepository.UpdateInvitation(accept, eventId);
        }

        public DataTable GetAllData(string username)
        {
            return _repository.ListData(username);
        }
        public bool exist(string userId, int eventId)
        {
            return _innerRepository.exist(userId, eventId);
        }
    }
}