using Soranmakale.Core.InterfaceDataAccess;
using Soranmakale.DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.BusinessLayer.Abstract
{
    public abstract class BaseManager<T> : IDataAccess<T> where T : class
    {
        private Repository<T> repo = new Repository<T>();

        public virtual int Delete(T obj)
        {
            return repo.Delete(obj);
        }

        public virtual T Find(System.Linq.Expressions.Expression<Func<T, bool>> kosul)
        {
            return repo.Find(kosul);
        }

        public virtual int Insert(T obj)
        {
            return repo.Insert(obj);
        }

        public virtual IQueryable<T> IQueryableList()
        {
            return repo.IQueryableList();
        }

        public virtual List<T> List()
        {
            return repo.List();
        }

        public virtual List<T> List(System.Linq.Expressions.Expression<Func<T, bool>> kosul)
        {
            return repo.List();
        }

        public virtual int Save()
        {
            return repo.Save();
        }

        public virtual int Update(T obj)
        {
            return repo.Update(obj);
        }
    }
}
