namespace Rentora.Application.IServices
{
    public interface ICartService
    {
        Task<bool> AddInCartAsync(string userId, int productId);
        Task<bool> RemoveFromCartAsync(string userId, int productId);
        Task<(IReadOnlyCollection<int>, int)> GetUserCartItemsPaginatedAsync
            (string userId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
