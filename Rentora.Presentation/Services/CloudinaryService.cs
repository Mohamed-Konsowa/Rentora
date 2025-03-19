using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace Rentora.Presentation.Services
{
    public class CloudinaryService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "RentoraImages",
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.AbsoluteUri;
        }

        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            string publicId = ExtractPublicId(imageUrl);
            if (string.IsNullOrEmpty(publicId))
                return false;

            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok";
        }
        public static string ExtractPublicId(string imageUrl)
        {
            var uri = new Uri(imageUrl);
            var pathSegments = uri.AbsolutePath.Split('/');

            // البحث عن "upload" ثم أخذ الجزء الذي بعده كـ PublicId
            int uploadIndex = Array.IndexOf(pathSegments, "upload");
            if (uploadIndex == -1 || uploadIndex + 2 >= pathSegments.Length) // +2 لأن الفهرس التالي هو رقم الإصدار
                return null;

            // تجاوز رقم الإصدار وأخذ باقي المسار كـ PublicId
            return string.Join("/", pathSegments.Skip(uploadIndex + 2)).Split('.')[0]; // إزالة الامتداد
        }
    }
}
