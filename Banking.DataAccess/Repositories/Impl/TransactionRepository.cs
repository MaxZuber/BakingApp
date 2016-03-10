using System;
using System.Collections.Generic;
using System.Linq;
using Banking.DataAccess.DataContext;
using Banking.Models;

namespace Banking.DataAccess.Repositories.Impl
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DataAccsessContext dataAccessContext) : base(dataAccessContext)
        {
        }

        public override Transaction Get(int id)
        {
            return _db.Transactions.SingleOrDefault(x => x.Id == id);
        }

        public override Transaction Save(Transaction entity)
        {
            _db.Transactions.Add(entity);
            return entity;
        }

        public override Transaction Update(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
