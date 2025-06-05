using Microsoft.EntityFrameworkCore;
using Rentora.Application.IRepositories;
using Rentora.Domain.Models;
using Rentora.Persistence.Data.DbContext;

namespace Rentora.Persistence.Repositories
{
    public class CartRepository : Repository<RentalCart>, ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<RentalCart> GetCartAsync(string userId, int productId)
        {
            return await _context.RentalCarts.FirstOrDefaultAsync
                (c => c.ApplicationUserId == userId && c.ProductId == productId);
        }

        public async Task<List<int>> GetUserCartItemsAsync(string userId)
        {
            return await _context.RentalCarts
                .Where(c => c.ApplicationUserId == userId)
                .Select(c => c.ProductId)
                .ToListAsync();
        }
    }
}
