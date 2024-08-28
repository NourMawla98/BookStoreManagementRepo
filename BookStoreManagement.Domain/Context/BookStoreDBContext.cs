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
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }

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

                // Configure TotalPrice with appropriate column type
                entity.Property(p => p.TotalPrice)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");

                // Configure PurchaseDate
                entity.Property(p => p.PurchaseDate)
                      .IsRequired();

                // Configure one-to-many relationship with PurchaseDetail
                entity.HasMany(p => p.PurchaseDetails)
                      .WithOne(pd => pd.Purchase)
                      .HasForeignKey(pd => pd.PurchaseId)
                      .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete
            });

            modelBuilder.Entity<PurchaseDetail>(entity =>
            {
                // Define the primary key
                entity.HasKey(pd => pd.PurchaseDetailId);

                // Configure BookId, Quantity, and Price
                entity.Property(pd => pd.BookId)
                      .IsRequired();

                entity.Property(pd => pd.Quantity)
                      .IsRequired();

                entity.Property(pd => pd.Price)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");

                // Configure foreign key relationship with Purchase
                entity.HasOne(pd => pd.Purchase)
                      .WithMany(p => p.PurchaseDetails)
                      .HasForeignKey(pd => pd.PurchaseId)
                      .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
