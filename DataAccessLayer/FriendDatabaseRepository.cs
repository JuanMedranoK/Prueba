using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FriendDatabaseRepository : BaseRepository, IRepository<Friend>
    {
        public FriendDatabaseRepository(SqlConnection connection) : base(connection)
        {

        }
        public bool Add(Friend item)
        {
            SqlCommand command = new SqlCommand("insert into Tbl_Friends(UserId, FriendId) values(@userId,@friendId)", GetConnection());

            command.Parameters.AddWithValue("@userId", item.UserId);
            command.Parameters.AddWithValue("@friendId", item.FriendId);

            return executeDml(command);
        }

        public bool Delete(int id)
        {
            SqlCommand command = new SqlCommand("delete Tbl_Friends where id = @id", GetConnection());

            command.Parameters.AddWithValue("@id", id);

            return executeDml(command);
        }

        public bool DeleteFriend(string username, string FriendUserName)
        {
            SqlCommand command = new SqlCommand("delete Tbl_Friends where UserId = '"+username+ "' and FriendId='"+FriendUserName+ "' or FriendId='"+username+"' and UserId='"+FriendUserName+"'", GetConnection());
            return executeDml(command);
        }
        public int GetbyId(string username)
        {
            int count = 0;

            GetConnection().Open();

            using (SqlCommand command = new SqlCommand("select count(*) from tbl_users where username in(select FriendId from Tbl_Friends f inner join tbl_users u on f.UserId = u.username where u.username = '" + username+"')", GetConnection()))
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

        public int GetbyPostId(string username)
        {
            int count = 0;

            GetConnection().Open();

            using (SqlCommand command = new SqlCommand("select count(*) from Tbl_Post where UserName in(select FriendId from Tbl_Friends f inner join tbl_users u on f.UserId = u.username where u.username = '" + username+"')", GetConnection()))
                

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

        public DataSet List(string userId)
        {
            string sqlDataAdapter = string.Format("select * from tbl_users where username in(select FriendId from Tbl_Friends f inner join tbl_users u on f.UserId = u.username where u.username='{0}')", userId);

            return Get(sqlDataAdapter);
        }

        public bool exist(string username, string FriendUserName)
        {
            string command = string.Format("select UserId from Tbl_Friends where FriendId='{0}'", FriendUserName);

            DataSet ds = Get(command);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string UserId = ds.Tables[0].Rows[0]?["UserId"].ToString().Trim();
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

        public DataSet PostList(string username)
        {
            string sqlDataAdapter = string.Format("select * from Tbl_Post p inner join Tbl_Users u on u.UserName=p.UserName where p.UserName in(select FriendId from Tbl_Friends f inner join tbl_users u on f.UserId = u.username where u.username = '"+username+"')");

            return Get(sqlDataAdapter);
        }
        public DataTable ListDataPhoto()
        {
            SqlDataAdapter query = new SqlDataAdapter("SELECT p.id as postId, u.username, p.PhotoPublish, u.photo FROM Tbl_PostImage p JOIN Tbl_Users u  on p.username = u.username ", GetConnection());
            return LoadData(query);
        }
        public bool Update(Friend item)
        {
            SqlCommand command = new SqlCommand("update Tbl_Friends set UserId=@userId where Id = @id",
               GetConnection());

            command.Parameters.AddWithValue("@id", item.Id);
            command.Parameters.AddWithValue("@userId", item.UserId);

            return executeDml(command);
        }

        public DataTable ListData(string username)
        {
            SqlDataAdapter query = new SqlDataAdapter( String.Format("select * from tbl_users where username in(select FriendId from Tbl_Friends f inner join tbl_users u on f.UserId = u.username where u.username='{0}')", username), GetConnection());
            return LoadData(query); ;
        }
    }
}
