using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.UnitOfWork
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        Repository<T> EntityRepository { get; }
        void BeginTransaction();
        void CommitTransaction();
        void Save();
    }
}
