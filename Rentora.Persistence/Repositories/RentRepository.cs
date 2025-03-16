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

        public List<int> GetUserRents(string userId)
        {
            return _context.Rentals.Where(r => r.ApplicationUserId == userId).
                Select(r => r.ProductId).ToList();
        }
    }
}
