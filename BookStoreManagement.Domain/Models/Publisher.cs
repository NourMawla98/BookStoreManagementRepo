using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreManagement.Domain.Models
{
    [Table("publisher")]
    public class Publisher
    {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name"), Required, MaxLength(250)]
        public string Name { get; set; } = String.Empty;

        [Column("location"), Required, MaxLength(250)]
        public string Location { get; set; } = String.Empty;

       

        public virtual ICollection<BookPublisher> PublishedBooks { get; set; }

    }
}