using BookStoreManagement.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreManagement.Domain.Models
{
    [Table("book_publisher")]
    public class BookPublisher
    {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("book_id"), Required]
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public virtual Book Book { get; set; }

        [Column("publisher_id"), Required]
        public int PublisherId { get; set; }

        [ForeignKey(nameof(PublisherId))]
        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        [Column("price"), Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }
    }
}
