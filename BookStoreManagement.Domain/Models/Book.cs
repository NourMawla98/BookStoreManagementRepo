using BookStoreManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreManagement.Domain.Models
{
    [Table("book")]
    public class Book
    {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("title"), Required, MaxLength(250)]
        public string Title { get; set; } = String.Empty;

        [Column("genre"), Required]
        public BookGenreEnum Genre { get; set; }

        [Column("auther_id")]
        public int AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual Author? Author { get; set; }

        public List<BookPublisher> Publishers { get; set; } = new List<BookPublisher>();
    }
}