using BookStoreManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreManagement.Domain.Context
{
    public class BookStoreDBContext : DbContext
    {
        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options) : base(options)
        {
        }

        #region DBSets

        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Publisher> Publisher { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookPublisher> BookPublisher { get; set; }
        public DbSet<Purchase> Purchase { get; set; }

        #endregion DBSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}