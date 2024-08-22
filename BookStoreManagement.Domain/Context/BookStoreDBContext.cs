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
        public virtual DbSet<Purchase> Purchase { get; set; } // Add Purchase DbSet

        #endregion DBSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the BookPublisher entity
            modelBuilder.Entity<BookPublisher>(entity =>
            {
                entity.HasKey(bp => bp.Id);// yaane ll id lal bookpublisher huwe PK, W I guess this is a lamda expression

                entity.Property(bp => bp.Price)
                      .IsRequired() // Ensure price is required
                      .HasColumnType("decimal(18,2)"); // Define the type for decimal to be taken and stored in db


                //HasOne and WithMany byaamlu sawa one-to-one rlt between Book and Publisher. yaane each single book can have many entries(Rows) in BookPublisher table (ykun eendu many publishers.

                entity.HasOne(bp => bp.Book) //bookpublisher has one book
                      .WithMany(b => b.Publishers) // book has many publishers
                      .HasForeignKey(bp => bp.BookId); //bookpublisher has a foreign key


                //HasOne and WithMany byaamlu sawa one-to-one rlt between Book and Publisher. yaane each single publisher can have many entries(Rows) in BookPublisher table (ykun eendu many published books).

                entity.HasOne(bp => bp.Publisher) //bookpublisher has one publisher
                      .WithMany( p => p.PublishedBooks) //publsiher has many publsihedBooks
                      .HasForeignKey(bp => bp.PublisherId); //bookpublisher has a foreign key
            });

            // Configure the Purchase entity
            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(p => p.PurchaseId);

                entity.Property(p => p.TotalPrice)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");

                entity.HasOne(p => p.BookPublisher)
                      .WithMany(bp => bp.Purchases)
                      .HasForeignKey(p => p.BookPublisherId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
