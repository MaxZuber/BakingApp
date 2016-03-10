using System;
using System.Data;
using Banking.DataAccess.Repositories;
using Banking.Models;

namespace Banking.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; } 
        ITransactionRepository TransactionRepository { get; }
        void BeginTransaction(IsolationLevel isolationLevel);
        void Commit();
        void Rollback();
    }
}
