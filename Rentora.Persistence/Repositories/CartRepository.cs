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
    public class CartRepository : Repository<RentalCart>, ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public RentalCart GetCart(string userId, int productId)
        {
            return _context.RentalCarts.FirstOrDefault
                (c => c.ApplicationUserId == userId && c.ProductId == productId);
        }

        public  List<int> GetUserCartItems(string userId)
        {
            return _context.RentalCarts.Where
                (c => c.ApplicationUserId == userId).Select(c => c.ProductId).ToList();
        }
    }
}
