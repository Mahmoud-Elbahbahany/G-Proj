using DAL.Generic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Generic.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Properties
        private readonly List<T> List; // this's the List of T class to handle the runtime Methods Processing with the Db.
        private readonly List<T> ExistedList; //this's the List of exisiting T(objects) in the Db.
        //private readonly ManualDbContext; //there will be component to handle the Methods Processing with the Db (Manually => without using EntityFrameWork of .Net).
        #endregion

        Repository()
        {
            List = new List<T>();
            ExistedList = new List<T>();
            //ManualDbContext = new ManualDbContext() // this will be initialization generic based on the T of repository.
        }



        #region Methods
        public void Create(T entity)
        {
            this.List.Add(entity);
        }

        public void Delete(T entity)
        {
            this.List.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return this.ExistedList;
        }

        public T GetByID(int id)
        {
            //return (T) this.ExistedList[id];
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
