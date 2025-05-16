using Microsoft.EntityFrameworkCore;
using Rentora.Application.IRepositories;
using Rentora.Domain.Models;
using Rentora.Persistence.Data.DbContext;

namespace Rentora.Persistence.Repositories
{
    public class FavoriteRepository : Repository<Favorite>, IFavoriteRepository
    {
        private readonly ApplicationDbContext _context;
        public FavoriteRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<Favorite> GetFavoriteAsync(string userId, int productId)
        {
            return await _context.Favorites
                .FirstOrDefaultAsync(c => c.ApplicationUserId == userId && c.ProductId == productId);
        }

        public async Task<List<int>> GetUserFavoriteItemsAsync(string userId)
        {
            return await _context.Favorites
                .Where(c => c.ApplicationUserId == userId)
                .Select(c => c.ProductId)
                .ToListAsync();
        }
    }
}
