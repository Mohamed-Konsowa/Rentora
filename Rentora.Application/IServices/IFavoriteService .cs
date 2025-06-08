namespace Rentora.Application.IServices
{
    public interface IFavoriteService
    {
        Task<bool> AddInFavoriteAsync(string userId, int productId);
        Task<bool> RemoveFromFavoriteAsync(string userId, int productId);
        Task<(IReadOnlyCollection<int>, int)> GetUserFavoriteItemsPaginatedAsync
            (string userId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
