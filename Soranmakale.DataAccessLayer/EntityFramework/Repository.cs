using Soranmakale.Core.InterfaceDataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.DataAccessLayer.EntityFramework
{
    public class Repository<T> : RepositoryBase , IDataAccess<T> where T : class
    {
        private DbSet<T> dbset;

        public Repository()
        {
            dbset = context.Set<T>();
        }

        public List<T> List()
        {
            return dbset.ToList();
        }

        public List<T> List(Expression<Func<T,bool>> kosul)
        {
            return dbset.Where(kosul).ToList();
        }

        public IQueryable<T> IQueryableList()
        {
            return dbset.AsQueryable<T>();
        }

        public int Insert(T obj)
        {
            dbset.Add(obj);

            return Save();
        }

        public int Update(T obj)
        {
            return Save();
        }

        public int Save()
        {
            return context.SaveChanges();

        }

        public int Delete(T obj)
        {
            dbset.Remove(obj);

            return Save();
        }

        public T Find(Expression<Func<T, bool>> kosul)
        {
            return dbset.Where(kosul).FirstOrDefault();
        }
    }
}
