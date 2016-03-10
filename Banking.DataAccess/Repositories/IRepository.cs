namespace Banking.DataAccess.Repositories
{
    public interface IRepository<TEntity>
    {
        TEntity Get(int id);
        TEntity Save(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(int id);

    }
}
