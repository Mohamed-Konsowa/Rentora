using System.ComponentModel.DataAnnotations;

namespace Rentora.Application.DTOs.Authentication
{
    public class AddRoleModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Role { get; set; }

    }
}
