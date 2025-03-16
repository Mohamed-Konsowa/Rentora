using Rentora.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentora.Application.IRepositories
{
    public interface IFavoriteRepository : IRepository<Favorite>
    {
        List<int> GetUserFavoriteItems(string userId);
        Favorite GetFavorite(string userId, int productId);
    }
}
