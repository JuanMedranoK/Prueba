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
    public class UserDatabaseRepository : BaseRepository, IUserRepository<User>
    {
    
        public UserDatabaseRepository(SqlConnection connection) : base(connection)
        {

        }
        public bool Add(User item)
        {
            SqlCommand command = new SqlCommand("insert into Tbl_Users(Name, LastName, UserName, Password, Phone, Mail) values(@name,@lastname,@username,@password,@phone, @mail)", GetConnection());

            command.Parameters.AddWithValue("@name", item.Name);
            command.Parameters.AddWithValue("@lastname", item.LastName);
            command.Parameters.AddWithValue("@username", item.UserName);
            command.Parameters.AddWithValue("@password", item.Password);
            command.Parameters.AddWithValue("@phone", item.Phone);
            command.Parameters.AddWithValue("@mail", item.Mail);
       


            return executeDml(command);
        }

        public void CreateSession(int userId)
        {
            SqlCommand command = new SqlCommand("insert into Sesion(UserId) values(@userId)", GetConnection());


            command.Parameters.AddWithValue("@userId", userId);
            executeDml(command);
        }
        public bool exist(string username, string mail)
        {
            string command = string.Format("select username from Tbl_Users where username='{0}'", username);

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

        public bool existMail(string username, string mail)
        {
            string command = string.Format("select * from Tbl_Users where username='{0}'", username);

            DataSet ds = Get(command);
            if (ds.Tables[0].Rows.Count > 0)
            {
       
                string Email = ds.Tables[0].Rows[0]?["mail"].ToString().Trim();
                if (mail == Email)
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
        public User GetUserSession()
        {
            var sql = "SELECT u.* FROM Sesion s JOIN Tbl_Users u on u.id = s.userId";
            var session = Query(null, sql).FirstOrDefault();

            return session;
        }

        public void DeleteUserSession(int userId)
        {
            var cmd = new SqlCommand("DELETE FROM Sesion", GetConnection());
            executeDml(cmd);
        }

        public bool PasswordRestore(User user)
        {

            SqlCommand command = new SqlCommand("UPDATE Tbl_Users SET Password=@password WHERE Mail = @mail",
               GetConnection());

            command.Parameters.AddWithValue("@mail", user.UserName);
            command.Parameters.AddWithValue("@password", user.Password);



            return executeDml(command);


        }
        public bool Getting(string name, string username)
        {
            string command = string.Format("SELECT * FROM Tbl_Users WHERE USERNAME='{0}'", name);
            DataSet ds = Get(command);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string Username = ds.Tables[0].Rows[0]?["UserName"].ToString().Trim();
                if (Username == name)
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
  

        public bool validPass(string username, string password)
        {
            var parameters = new QueryPrameter[2];
            parameters[0] = new QueryPrameter("username", username);
            parameters[1] = new QueryPrameter("password", password);

            var result = Query(parameters).FirstOrDefault();
            
            if (result != null)
            {

                SetUserSession(result);
                return true;
            }

            return false;
        }
        private void SetUserSession(User user)
        {
            UserSession.SetUserSession(user);
            CreateSession(user.Id);
        }
        public bool LoadSession()
        {
            var user = GetUserSession();

            if (user == null)
            {
                return false;
            }

            SetUserSession(user);
            return true;
        }
        public void Logout()
        {
            DeleteUserSession(UserSession.CurrentSession.User.Id);
            UserSession.SetUserSession(null);
        }
        public User GetById(int id)
        {
            var parameters = new QueryPrameter[]{
                new QueryPrameter("id", id)
            };

            var results = Query(parameters);

            return results.FirstOrDefault();
        }
        public List<User> Query(QueryPrameter[] parameters = null, string baseQuery = "SELECT * FROM Tbl_Users")
        {
            var query = GenerateQueryCommandd(parameters, baseQuery);

            var reader = query.ExecuteReader();

            var result = new List<User>();

            while (reader.Read())
            {
                var user = new User
                {
                    Id = (int)reader.GetValue(0),
                    Name = (string)reader.GetValue(1),
                    LastName = (string)reader.GetValue(2),
                    UserName = (string)reader.GetValue(3),
                    Phone = (string)reader.GetValue(4),
                    Mail = (string)reader.GetValue(5)
                };

                result.Add(user);
            }

            query.Connection.Close();

            return result;
        }

        private SqlCommand GenerateQueryCommandd(QueryPrameter[] parameters, string baseQuery)
        {
            var sql = baseQuery;

            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];

                    if (i == 0) sql += " WHERE ";
                    else sql += " AND ";

                    sql += parameter.name + "=" + "@" + parameter.name;
                }
            }

            var con = GetConnection();

            con.Open();

            var query = new SqlCommand(sql);

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    query.Parameters.AddWithValue("@" + parameter.name, parameter.value);
                }
            }

            query.Connection = con;
            return query;
        }

        public bool SavePhoto(int id, string destination)
        {
            SqlCommand command = new SqlCommand("update Tbl_Users set Photo=@photo where Id = @id",
                      GetConnection());

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@photo", destination);

            return executeDml(command);
        }

        public int GetLastId()
        {
            int lastId = 0;

            GetConnection().Open();

            using (SqlCommand command = new SqlCommand("select max(Id) as Id from Tbl_Users", GetConnection()))
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

    }
    public struct QueryPrameter
    {

        public string name;
        public object value;

        public QueryPrameter(string name, object value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
