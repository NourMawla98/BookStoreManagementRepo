using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreManagement.Domain.Models
{
    [Table("book_publisher")]
    public class BookPublisher
    {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("book_id")]
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public virtual Book Book { get; set; }

        [Column("publisher_id")]
        public int PublisherId { get; set; }

        [ForeignKey(nameof(PublisherId))]
        public virtual Publisher Publisher { get; set; }
    }
}