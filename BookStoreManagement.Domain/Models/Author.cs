using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreManagement.Domain.Models
{
    [Table("author")]
    public class Author
    {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name"), Required, MaxLength(250)]
        public string Name { get; set; } = String.Empty;

        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; } = null;

        public IList<Book> Books { get; set; } = new List<Book>();

        [NotMapped]
        public int NumberOfBooks
        {
            get
            {
                return Books.Count;
            }
        }
    }
}