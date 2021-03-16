using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
   public interface IRepository<T>
    {
        bool Add(T item);
        bool Update(T item);
        bool Delete(int id);
        DataSet List(string username);
        DataTable ListData(string username);
  
  
    }
}
