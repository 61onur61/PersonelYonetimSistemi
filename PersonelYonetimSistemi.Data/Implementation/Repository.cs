using Microsoft.EntityFrameworkCore;
using PersonelYonetimSistemi.Data.Contracts;
using PersonelYonetimSistemi.Data.DataContext;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PersonelYonetimSistemi.Data.Implementation
{
    public class Repository<T> : IRepositoryBase<T> where T : class, new()
    {
        private readonly PersonelYonetimSistemiContext _context;
        internal DbSet<T> dbSet;

        public Repository(PersonelYonetimSistemiContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }
        /// <summary>
        /// Gelen Tipte veriyi kaydeder.
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Gelen Tipte Veriyi id değerine göre bulur.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public T Get(int Id)
        {
            return dbSet.Find(Id);
        }

        /// <summary>
        /// Bir tablodaki bütün verileri getirir.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            if(orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
           dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
