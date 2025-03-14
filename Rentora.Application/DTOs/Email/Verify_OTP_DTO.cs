using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentora.Application.DTOs.Email
{
    public class Verify_OTP_DTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string OTPCode { get; set; }
    }
}
