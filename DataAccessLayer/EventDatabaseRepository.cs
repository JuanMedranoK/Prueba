using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EventDatabaseRepository : BaseRepository, IRepository<Event>
    {
        public EventDatabaseRepository(SqlConnection connection) : base(connection)
        {

        }

        public bool Add(Event item)
        {
            SqlCommand command = new SqlCommand("insert into Tbl_Events(Name, CreationDate, State, UserName, Place) values(@name,@creationDate,@state, @username, @place)", GetConnection());

            command.Parameters.AddWithValue("@name", item.Name);
            command.Parameters.AddWithValue("@creationDate", item.CreationDate);
            command.Parameters.AddWithValue("@state", item.State);
            command.Parameters.AddWithValue("@username", item.UserName);
            command.Parameters.AddWithValue("@place", item.Place);
     
    
           return executeDml(command);
        }

        public bool AddEvenInvitation(Event item)
        {
            SqlCommand command = new SqlCommand("insert into Tbl_Events_Invitation(EventId,UserName, Accept) values(@eventId,@userName,@accept)", GetConnection());

            command.Parameters.AddWithValue("@eventId", item.Id);
            command.Parameters.AddWithValue("@userName", item.UserName);
            command.Parameters.AddWithValue("@accept", item.Accept);
          
            return executeDml(command);
        }
        public bool exist(string username, int eventId)
        {
            string command = string.Format("select * from Tbl_Events_Invitation where eventId={0} and username='{1}'", eventId, username);

            DataSet ds = Get(command);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string UserId = ds.Tables[0].Rows[0]?["username"].ToString().Trim();
                if (username == UserId)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        public int GetLastId()
        {
            int lastId = 0;

            GetConnection().Open();

            using (SqlCommand command = new SqlCommand("select max(Id) as Id from Tbl_Events", GetConnection()))
            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    lastId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                }
            }
            GetConnection().Close();
            return lastId;
        }
        public bool Delete(int id)
        {
            SqlCommand command = new SqlCommand("delete Tbl_Events where id = @id", GetConnection());

            command.Parameters.AddWithValue("@id", id);


            return executeDml(command);
        }

        public bool DeleteFromEvent(string username, int eventId)
        {
            SqlCommand command = new SqlCommand("delete  Tbl_Events_Invitation where eventId = @id and username=@username", GetConnection());

            command.Parameters.AddWithValue("@id", eventId);
            command.Parameters.AddWithValue("@username", username);


            return executeDml(command);
        }

        public int GetbyId(string username)
        {
            int count = 0;

            GetConnection().Open();

            using (SqlCommand command = new SqlCommand("select count(*) from Tbl_Events where username='" + username + "'", GetConnection()))
            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    count = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                }

            }

            GetConnection().Close();

            return count;
        }
        public int GetEventsFriendsCountById(string username)
        {
            int count = 0;

            GetConnection().Open();

            using (SqlCommand command = new SqlCommand(" select count( *) from [Tbl_Events] where Id in(select EventId from Tbl_Events_Invitation where UserName='" + username + "')", GetConnection()))
            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    count = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                }

            }

            GetConnection().Close();

            return count;
        }
        public int GetInvitations(int eventId)
        {
            int count = 0;

            GetConnection().Open();

            using (SqlCommand command = new SqlCommand("select count(*) from Tbl_Events_Invitation where EventId='" + eventId + "'", GetConnection()))
            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    count = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                }

            }

            GetConnection().Close();

            return count;
        }
        public int GetfriendsbyId(int eventId)
        {
            int count = 0;

            GetConnection().Open();

            using (SqlCommand command = new SqlCommand("select count(*) from Tbl_Events_Invitation where EventId=" + eventId + "", GetConnection()))
            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    count = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                }

            }

            GetConnection().Close();

            return count;
        }

   

        public DataSet List(string username)
        {
            string sqlDataAdapter = string.Format("select * from Tbl_Events where username='{0}'", username);

            return Get(sqlDataAdapter);
        }
        public DataSet GetFriendsList(int eventId)
        {
            string sqlDataAdapter = string.Format("  select * from Tbl_Events_Invitation i inner join Tbl_Users u on u.UserName=i.UserName where EventId=" + eventId);

            return Get(sqlDataAdapter);
        }

        public DataSet GetFriendEventList(string username)
        {
            string sqlDataAdapter = string.Format("select * from Tbl_Events where Id in(select EventId from Tbl_Events_Invitation where UserName='" + username+"')");

            return Get(sqlDataAdapter);
        }



     
        public bool Update(Event item)
        {
            SqlCommand command = new SqlCommand("update Tbl_Events set Name=@name where Id = @id",
                   GetConnection());

            command.Parameters.AddWithValue("@id", item.Id);
            command.Parameters.AddWithValue("@name", item.Name);

            return executeDml(command);
        }
            public bool UpdateInvitation(int accept, int eventId)
        {
            SqlCommand command = new SqlCommand("update Tbl_Events_Invitation set Accept=@accept where EventId = @id",
                         GetConnection());

            command.Parameters.AddWithValue("@id", eventId);
            command.Parameters.AddWithValue("@accept", accept);

            return executeDml(command);
        }

        public DataTable ListData(string username)
        {
            SqlDataAdapter query = new SqlDataAdapter(string.Format("select * from Tbl_Events where username='{0}'", username), GetConnection());
            return LoadData(query);
        }
    }
}
