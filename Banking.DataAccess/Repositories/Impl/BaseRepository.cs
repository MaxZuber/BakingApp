using System.Collections.Generic;
using Banking.DataAccess.DataContext;

namespace Banking.DataAccess.Repositories.Impl
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
    {
        protected DataAccsessContext _db;
        protected BaseRepository(DataAccsessContext dataAccessContext)
        {
            _db = dataAccessContext;
        }
        public abstract TEntity Get(int id);

        public abstract TEntity Save(TEntity entity);

        public abstract TEntity Update(TEntity entity);

        public abstract void Delete(int id);
    }
}
