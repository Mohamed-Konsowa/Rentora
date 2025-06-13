using Microsoft.AspNetCore.Http;

namespace Rentora.Application.IServices
{
    public interface IImageService
    {
        Task<string?> UploadImageAsync(IFormFile? file);
        Task<bool> DeleteImageAsync(string imageUrl);
    }
}
