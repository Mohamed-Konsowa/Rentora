using Microsoft.AspNetCore.Http;

namespace Rentora.Application.Helpers
{
    public static class CommonUtils
    {
        public static async Task<string> ConvertImageToBase64(IFormFile image)
        {
            using var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();
            var profileImageBase64 = Convert.ToBase64String(imageBytes);
            return profileImageBase64;
        }
        public static bool IsValidEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
        public static (bool, string) IsImage(IFormFile file)
        {
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
            if (!allowedTypes.Contains(file.ContentType)) return (false, "Invalid file type. Only JPEG, PNG, and GIF are allowed.");
            return (true, "");
        }
    }
}
