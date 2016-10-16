using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Data.Infrastructure
{
    using System.Data.Entity;
    using System.Linq.Expressions;

    using Skyline.Data.Concrete;

    public abstract class RepositoryBase<T> where T : class
    {
        private SkylineDbContext _dataContext;

        private readonly IDbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get; set;
        }

        protected SkylineDbContext DbContext
        {
            get { return this._dataContext ?? (this._dataContext = DbFactory.Init()); }
        }

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            this.dbSet = DbContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            this.dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            this.dbSet.Attach(entity);
            this._dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            this.dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = this.dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
            {
                this.dbSet.Remove(obj);
            }
        }

        public virtual T GetById(int id)
        {
            return this.dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this.dbSet.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return this.dbSet.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return this.dbSet.Where(where).FirstOrDefault();
        }
    }
}
