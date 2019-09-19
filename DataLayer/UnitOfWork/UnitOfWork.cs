using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Repositories;
using System.Data.Entity;

namespace DataLayer.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly DbContext db;
        private bool disposed = false;

        Repository<T> entityRepository;
        public Repository<T> EntityRepository
        {
            get
            {
                return entityRepository == null ? new Repository<T>(db) : entityRepository;
            }
        }
        public UnitOfWork(DbContext model)
        {
            this.db = model;
            db.Database.CommandTimeout = 180;
        }

        public void BeginTransaction()
        {
            db.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            db.Database.CurrentTransaction.Commit();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                if (disposing)
                {

                    if (db != null)
                    {
                        db.Dispose();
                    }
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
