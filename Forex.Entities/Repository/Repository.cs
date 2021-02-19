using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forex.Entities.Repository
{
    public class Repository : IRepository
    {
        private readonly ForexDbContext _context;

        public Repository(ForexDbContext context)
        {
            _context = context;
        }

        public ForexDbContext DBContext
        {
            get { return _context; }
        }

        public T Add<T>(T entity) where T: class
        {
            var dbEntity = _context.Entry<T>(entity);
            dbEntity.State = EntityState.Added;
            return entity;
        }

        public void AddRange<T>(IEnumerable<T> entities) where T : class
        {
            _context.Set<T>().AddRange(entities);
        }

        public IQueryable<T> Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T: class
        {
            return _context.Set<T>().Where(expression);
        }

        public IQueryable<T> GetAll<T>() where T: class
        {
            return _context.Set<T>();
        }

        public void Remove<T>(T entity) where T: class
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange<T>(IEnumerable<T> entities) where T: class
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public T Update<T>(T entity) where T : class
        {
            var dbEntity = _context.Entry<T>(entity);
            dbEntity.State = EntityState.Modified;
            return entity;
        }
    }
}
