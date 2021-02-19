using Forex.Entities.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forex.Entities.UOW
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly IRepository _repository;
        private readonly ForexDbContext _context;

        public UnitOfWork(IRepository repository)
        {
            _repository = repository;
            _context = repository.DBContext;
        }

        public void Commit()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var t = _context.ChangeTracker.Entries().ToList();
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
