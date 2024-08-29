namespace BookStoreManagement.Domain.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        //add quantity and book id here
        //delete PurchaseDetails
        //Add proper annotations (see other tables)
    }
}