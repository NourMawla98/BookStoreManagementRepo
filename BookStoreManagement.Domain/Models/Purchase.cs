namespace BookStoreManagement.Domain.Models
{
    public class Purchase
    {
        public Guid PurchaseId { get; set; }
        public int BookPublisherId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDate { get; set; }

        // Navigation property
        public BookPublisher BookPublisher { get; set; }
    }
}
