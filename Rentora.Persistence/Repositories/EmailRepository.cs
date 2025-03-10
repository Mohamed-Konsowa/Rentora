using Rentora.Application.IRepositories;
using Rentora.Domain.Models;
using Rentora.Persistence.Data.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentora.Persistence.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ApplicationDbContext _context;
        public EmailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddOtp(OTP otp)
        {
            var r = await _context.OTPs.AddAsync(otp);
            return r != null;
        }
    }
}
