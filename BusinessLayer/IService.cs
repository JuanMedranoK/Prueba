using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IService<T>
    {
        void Add(T item);
        void Update(T item);
        void Delete(int index);
        DataSet GetAll(string username);
        DataTable GetAllData(string username);
   

    }
}
