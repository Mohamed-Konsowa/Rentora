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

        public Favorite GetFavorite(string userId, int productId)
        {
            return _context.Favorites.FirstOrDefault
                (c => c.ApplicationUserId == userId && c.ProductId == productId);
        }

        public  List<int> GetUserFavoriteItems(string userId)
        {
            return _context.Favorites.Where
                (c => c.ApplicationUserId == userId).Select(c => c.ProductId).ToList();
        }
    }
}
