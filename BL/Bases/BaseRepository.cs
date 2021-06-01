using BL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Bases
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {

        private DbContext DbContext;
        private DbSet<T> DbSet;

        public BaseRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("DbContext Is Null");
            }
            this.DbContext = dbContext;
            this.DbSet = DbContext.Set<T>();
        }


        #region Get All Data Methods
        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public IQueryable<T> GetAllSorted<TKey>(Expression<Func<T, TKey>> sortingExpression)
        {
            return DbSet.OrderBy<T, TKey>(sortingExpression);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query;
        }

        public bool GetAny(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = DbSet;
            bool result = false;
            if (filter != null)
            {
                result = query.Any(filter);
            }
            return result;
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return DbSet.FirstOrDefault(filter);
            }
            return null;
        }

        public T GetFirstOrDefault(string toInclude, Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return DbSet.Include(toInclude).FirstOrDefault(filter);
            }
            return null;
        }
        #endregion


        #region Get one record
        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual T GetById(long id)
        {
            return DbSet.Find(id);
        }

        #endregion


        #region CRUD Methods
        public virtual T Insert(T entity)
        {
            // bool returnVal = false;
            EntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
            //returnVal = true;
            return entity;
        }

        public virtual void InsertList(List<T> entityList)
        {
            DbSet.AddRange(entityList);
        }

        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void UpdateList(List<T> entityList)
        {
            foreach (T item in entityList)
            {
                Update(item);
            }
        }

        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }
        public virtual void DeleteList(List<T> entityList)
        {
            foreach (T item in entityList)
            {
                Delete(item);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }


        #endregion

        #region pageination
        public IQueryable<T> GetByPage(int pageNumber, int pageSize)
        {
            pageNumber = (pageNumber < 0) ? 1 : pageNumber;
            pageSize = (pageSize > 12 || pageSize < 0) ? 12 : pageSize;

            return DbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        }

        
        public IQueryable<T> GetByPage(int pageNumber, int pageSize , Expression<Func<T, bool>> filter )
        {
            pageNumber = (pageNumber < 0) ? 1 : pageNumber;
            pageSize = (pageSize > 12 || pageSize < 0) ? 12 : pageSize;

            return DbSet.Where(filter).Skip((pageNumber - 1) * pageSize).Take(pageSize);

        }
        #endregion

    }
}
