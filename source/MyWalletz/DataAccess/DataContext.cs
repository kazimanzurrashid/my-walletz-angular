namespace MyWalletz.DataAccess
{
    using System;
    using System.Data.Entity;

    using DomainObjects;

    public interface IDataContext : IDisposable
    {
        IDbSet<User> Users { get; }

        IDbSet<Category> Categories { get; }

        IDbSet<Account> Accounts { get; }

        IDbSet<Transaction> Transactions { get; }

        IDbSet<TEntity> GetSet<TEntity>() where TEntity : class;

        int SaveChanges();
    }

    public class DataContext : DbContext, IDataContext
    {
        private IDbSet<User> users;
        private IDbSet<Category> categories; 
        private IDbSet<Account> accounts; 
        private IDbSet<Transaction> transactions;

        public DataContext() : base("DefaultConnection")
        {
        }

        public IDbSet<User> Users
        {
            get
            {
                return users ?? (users = this.GetSet<User>());
            }
        }

        public IDbSet<Category> Categories
        {
            get
            {
                return categories ?? (categories = this.GetSet<Category>());
            }
        }

        public IDbSet<Account> Accounts
        {
            get
            {
                return accounts ?? (accounts = this.GetSet<Account>());
            }
        }

        public IDbSet<Transaction> Transactions
        {
            get
            {
                return transactions ?? (transactions = this.GetSet<Transaction>());
            }
        }

        public virtual IDbSet<TEntity> GetSet<TEntity>()
            where TEntity : class
        {
            return Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Accounts)
                .WithRequired(a => a.User)
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Categories)
                .WithRequired(c => c.User)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Transactions)
                .WithRequired(t => t.User)
                .HasForeignKey(t => t.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithRequired(t => t.Account)
                .HasForeignKey(t => t.AccountId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Transactions)
                .WithOptional(t => t.Category)
                .HasForeignKey(t => t.CategoryId)
                .WillCascadeOnDelete(false);
        }
    }
}