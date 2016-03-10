using System.Data.Entity;
using Banking.Common.Settings;
using Banking.Models;

namespace Banking.DataAccess.DataContext
{
    public class DataAccsessContext : DbContext
    {
        public DataAccsessContext()
            : base(AppSettings.ConnectionString)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DataAccsessContext>());
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*ACCOUNTS*/

            modelBuilder.Entity<Account>().HasKey(x => x.Id);

            modelBuilder.Entity<Account>().Property(x => x.Username).IsRequired();
            modelBuilder.Entity<Account>().Property(x => x.Username).HasMaxLength(50);

            modelBuilder.Entity<Account>().Property(x => x.Password).IsRequired();
            modelBuilder.Entity<Account>().Property(x => x.Password).HasMaxLength(50);

            modelBuilder.Entity<Account>().Property(x => x.RowVersion).IsRowVersion();


            /*TRANSACTIONS*/

            modelBuilder.Entity<Transaction>()
                .HasRequired(x => x.Account)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.AccountId);
        }
    }
}
