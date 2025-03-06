namespace RentoraAPI.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string ApplicationUserId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ApplicationUser ApplicationUser { get; set; }
    }
}
