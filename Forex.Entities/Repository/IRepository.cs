using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Forex.Entities.Repository
{
    public interface IRepository
    {
        ForexDbContext DBContext { get; }
        IQueryable<T> GetAll<T>() where T : class;
        IQueryable<T> Get<T>(Expression<Func<T, bool>> expression) where T: class;
        T Add<T>(T entity) where T : class;
        void AddRange<T>(IEnumerable<T> entities) where T : class;
        void Remove<T>(T entity) where T: class;
        void RemoveRange<T>(IEnumerable<T> entities) where T: class;
        T Update<T>(T entity) where T: class;
    }
}