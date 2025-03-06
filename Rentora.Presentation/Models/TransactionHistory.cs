namespace RentoraAPI.Models
{
    public class TransactionHistory
    {
        public int TransactionHistoryId { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ApplicationUser FromUser { get; set; }
        public ApplicationUser ToUser { get; set; }

    }
}
