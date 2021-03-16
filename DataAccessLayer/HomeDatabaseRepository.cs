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
    public class HomeDatabaseRepository : BaseRepository, IRepository<Home>
    {

        private int UserID
        {
            get
            {
                return UserSession.CurrentSession.User.Id;
            }
        }
        private string User
        {
            get
            {
                return UserSession.CurrentSession.User.UserName;
            }
        }
        public HomeDatabaseRepository(SqlConnection connection) : base(connection)
        {

        }
        public string getUserName()
        {
            string user = "";

            GetConnection().Open();

            SqlCommand command = new SqlCommand("select username from Tbl_Users where id=@id", GetConnection());
            command.Parameters.AddWithValue("@id", UserID);


            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    user = reader.IsDBNull(0) ? "" : reader.GetString(0);
                }

            }

            GetConnection().Close();

            return user;
        }

        public bool Add(Home item)
        {
         

            SqlCommand command = new SqlCommand("insert into Tbl_Post (content,CreationDate,UserName) " +
                                                "values(@contenido,@date,@userid)",
                                                GetConnection());

            command.Parameters.AddWithValue("@contenido", item.Content);
            command.Parameters.AddWithValue("@date", item.Date);
            command.Parameters.AddWithValue("@userId", item.UserName);


            return executeDml(command);
        }
        public bool AddPhoto(Home item)
        {
   

            SqlCommand command = new SqlCommand("insert into Tbl_PostImage (PhotoPublish,CreationDate,UserName) " +
                                                "values(@contenido,@date,@userid)",
                                                GetConnection());

            command.Parameters.AddWithValue("@contenido", item.PhotoPublish);
            command.Parameters.AddWithValue("@date", item.Date);
            command.Parameters.AddWithValue("@userId", item.UserName);


            return executeDml(command);
        }

        public bool Delete(int id)
        {
            SqlCommand command = new SqlCommand("DELETE Tbl_Post WHERE Id = @id AND username = @userId",
                GetConnection());

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@userId", User);

            return executeDml(command);
        }

        public bool SavePhoto(int id, string destination)
        {
            SqlCommand command = new SqlCommand("update Tbl_PostImage set PhotoPublish=@photo where Id = @id",
                      GetConnection());

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@photo", destination);

            return executeDml(command);
        }
        public int GetLastId()
        {
            int lastId = 0;

            GetConnection().Open();

            using (SqlCommand command = new SqlCommand("select max(Id) as Id from Tbl_PostImage", GetConnection()))
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
        public DataTable ListData(string username)
        {
            SqlDataAdapter query = new SqlDataAdapter("SELECT p.id as postId, u.username, p.content, u.photo FROM Tbl_Post p JOIN Tbl_Users u  on p.username = u.username WHERE p.username='" + username+"' order by p.id desc", GetConnection());
            return LoadData(query);
        }

        public DataTable ListDataPhoto(string username)
        {
            SqlDataAdapter query = new SqlDataAdapter("SELECT p.id as postId, u.username, p.PhotoPublish, u.photo FROM Tbl_PostImage p JOIN Tbl_Users u  on p.username = u.username WHERE p.username='" + username + "' order by p.id desc", GetConnection());
            return LoadData(query);
        }

        public bool Update(Home item)
        {
            SqlCommand command = new SqlCommand("UPDATE Tbl_Post SET  content= @contenido Date=@date WHERE Id = @id AND userId = @userId",
               GetConnection());

            command.Parameters.AddWithValue("@id", item.Id);
            command.Parameters.AddWithValue("@contenido", item.Content);
        
            command.Parameters.AddWithValue("@date", item.Date);
            command.Parameters.AddWithValue("@userId", UserID);

            return executeDml(command);
        }

        public DataSet List(string username)
        {
       string query = "SELECT p.id as postId, u.username, p.contenido, u.photo FROM Tbl_Post p JOIN Tbl_Users u  on p.userId = u.id WHERE userId=" + UserID;
            return Get(query);
        }

     
    }
}