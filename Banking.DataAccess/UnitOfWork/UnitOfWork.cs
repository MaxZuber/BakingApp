using System;
using System.Data;
using System.Data.Entity;
using Banking.Common.IoC;
using Banking.DataAccess.DataContext;
using Banking.DataAccess.Repositories;
using Microsoft.Practices.Unity;

namespace Banking.DataAccess.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {

        private readonly DataAccsessContext _context = new DataAccsessContext();
        private IUserRepository _userRepository;
        private ITransactionRepository _transactionRepository;
        private bool _disposed = false;
        private DbContextTransaction _transaction;

        public IUserRepository UserRepository
        {
            get {
                return _userRepository ??
                       (_userRepository = Ioc.Get<IUserRepository>(new ParameterOverride("context", _context)));
            }
        }

        public ITransactionRepository TransactionRepository
        {
            get
            {
                return _transactionRepository ??
                       (_transactionRepository = Ioc.Get<ITransactionRepository>(new ParameterOverride("dataAccessContext", _context)));
            }
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
           _transaction = _context.Database.BeginTransaction(isolationLevel);
        }
        public void Commit()
        {
            
            _context.SaveChanges();

            if (_transaction != null)
            {
                _transaction.Commit();
            }

        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
            }
            
            Dispose();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
