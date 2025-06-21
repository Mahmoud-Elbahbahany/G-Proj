using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Generic.Interface
{
    public interface IRepository<T> where T : class
    {
        void Create (T entity);
        void Update (T entity);
        void Delete (T entity);
        T GetByID(int id);
        IEnumerable<T> GetAll ();
    }
}
