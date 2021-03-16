using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BaseRepository
    { 
     private readonly SqlConnection _connection;

    public BaseRepository(SqlConnection connection)
    {
        this._connection = connection;
    }

    public SqlConnection GetConnection()
    {
        return _connection;
    }
    public bool executeDml(SqlCommand query)
    {
        try
        {
            _connection.Open();

            query.ExecuteNonQuery();

            _connection.Close();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }


    }

    public DataSet Get(string query)
    {
        try
        {
            _connection.Open();

            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
            adapter.Fill(ds);
            _connection.Close();

            return ds;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public DataTable LoadData(SqlDataAdapter query)
    {
        try
        {
            DataTable data = new DataTable();
            _connection.Open();

            query.Fill(data);

            _connection.Close();

            return data;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }


    }

}

}
