using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentora.Application.DTOs.Email
{
    public class SendEmailDTO
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
    }
}
