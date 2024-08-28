namespace BookStoreManagement.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Purchase
    {
        [Key]
        public Guid PurchaseId { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        // Navigation property for PurchaseDetails
        public ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
    }

}
