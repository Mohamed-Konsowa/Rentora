namespace Rentora.Domain.Models
{
    public class ActivityLog
    {
        public int ActivityLogId { get; set; }
        public string ApplicationUserId { get; set; }
        public string Action { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ApplicationUser ApplicationUser { get; set; }
    }
}
