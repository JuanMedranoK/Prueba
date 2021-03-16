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
    public class CommentDatabaseRepository : BaseRepository, IRepository<Comment>
    {
        public CommentDatabaseRepository(SqlConnection connection) : base(connection)
        {
        }

        public bool Add(Comment item)
        {
            SqlCommand command = new SqlCommand("insert into Tbl_Comments(PostId, Comment, UserName, FriendId) values(@postId,@comment,@username,@friendId)", GetConnection());

            command.Parameters.AddWithValue("@postId", item.PostId);
            command.Parameters.AddWithValue("@comment", item.Comments);
            command.Parameters.AddWithValue("@username", item.UserName);
            command.Parameters.AddWithValue("@friendId", item.FriendId);
    
            return executeDml(command);
        }

        public bool Delete(int id)
        {
            SqlCommand command = new SqlCommand("delete Tbl_Comments where id = @id", GetConnection());

            command.Parameters.AddWithValue("@id", id);


            return executeDml(command);
        }

        public int GetbyId(string username)
        {
            int count = 0;

            GetConnection().Open();

            using (SqlCommand command = new SqlCommand(" select count(*) from Tbl_Comments where username = '"+ username + "'", GetConnection()))
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

        public int GetbyIdFriends()
        {
            int count = 0;

            GetConnection().Open();

            using (SqlCommand command = new SqlCommand("select count(*) from Tbl_Comments", GetConnection()))
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
            string sqlDataAdapter = string.Format(" select * from Tbl_Comments where username='" + username + "'");

            return Get(sqlDataAdapter);
        }

        public DataTable ListData(string username)
        {
            SqlDataAdapter query = new SqlDataAdapter(" select * from Tbl_Comments where username='"+ username + "'", GetConnection());
            return LoadData(query); ;
        }
        public DataSet ListDataFriends()
        {
            string sqlDataAdapter = string.Format(" select * from Tbl_Comments");

            return Get(sqlDataAdapter);
        }

        public bool Update(Comment item)
        {

            SqlCommand command = new SqlCommand("update Tbl_Comments set Comment=@comment where Id = @id",
                      GetConnection());

            command.Parameters.AddWithValue("@id", item.Id);
            command.Parameters.AddWithValue("@comment", item.Comments);

            return executeDml(command);
        }
    }
}
