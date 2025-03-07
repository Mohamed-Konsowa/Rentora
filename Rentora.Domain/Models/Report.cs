namespace Rentora.Domain.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public string ReporterUserId { get; set; }
        public string ReportedUserId { get; set; }
        public int ProductId { get; set; }
        public string Message { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ApplicationUser ReporterUser { get; set; }
        public ApplicationUser ReportedUser { get; set; }
        public Product Product { get; set; }
    }
}
