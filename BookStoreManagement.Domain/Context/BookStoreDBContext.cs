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
                      .WithMany(p => p.PublishedBooks) //publsiher has many publsihedBooks
                      .HasForeignKey(bp => bp.PublisherId); //bookpublisher has a foreign key
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                // Define the primary key
                entity.HasKey(p => p.PurchaseId);

                // Define properties and their configurations
                entity.Property(p => p.PurchaseDate)
                      .IsRequired() // Ensure PurchaseDate is required
                      .HasColumnType("datetime(6)"); // Define column type

                entity.Property(p => p.BookId)
                      .IsRequired(); // Ensure BookId is required

                entity.Property(p => p.Bookprice)
                      .IsRequired() // Ensure Bookprice is required
                      .HasColumnType("double"); // Define column type for double

                entity.Property(p => p.Quantity)
                      .IsRequired() // Ensure Quantity is required
                      .HasColumnType("int"); // Define column type for int



                // Configure other relationships if needed
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
