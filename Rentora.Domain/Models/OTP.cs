using System.ComponentModel.DataAnnotations;

namespace Rentora.Domain.Models
{
    public class OTP
    {
        public int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(4)]
        public string Code { get; set; }
        public DateTime ExpiryTime { get; set; } = DateTime.Now.AddMinutes(10);
        public bool IsUsed { get; set; } = false;

    }
}
