using Microsoft.EntityFrameworkCore;
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
    public class RentRepository : Repository<Rental>, IRentRepository
    {
        private readonly ApplicationDbContext _context;

        public RentRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<int>> GetUserRents(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;
            return await _context.Rentals
                .AsNoTracking()
                .Where(r => r.ApplicationUserId == userId)
                .Select(r => r.ProductId)
                .ToListAsync();
        }
    }
}
