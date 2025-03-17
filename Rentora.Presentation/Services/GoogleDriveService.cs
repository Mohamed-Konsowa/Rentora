using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace Rentora.Presentation.Services
{
    class GoogleDriveServicex
    {
        private static readonly string[] Scopes = { DriveService.Scope.DriveFile };
        private const string ApplicationName = "Rentora";
        private const string CredentialsPath = "wwwroot/Resourses/rentora-453721-0bd2cc9b6341.json"; // تأكد أن الملف في هذا المسار

        private static DriveService GetDriveService()
        {
            GoogleCredential credential;
            using (var stream = new FileStream(CredentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }
        public static async Task<string> UploadImageAsync(IFormFile file, string folderId = "1kBrUx090uTdeHUx2ZUzDGTwTO0Ebx8Dh")
        {
            try
            {
                // تحميل بيانات المصادقة
                GoogleCredential credential;
                using (var stream = new FileStream(CredentialsPath, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
                }

                using var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                // تحويل IFormFile إلى Stream
                using var fileStream = file.OpenReadStream();

                // إعداد البيانات الوصفية للملف
                var fileMetadata = new Google.Apis.Drive.v3.Data.File
                {
                    Name = file.FileName,
                    Parents = new[] { folderId } // حدد المجلد الذي سيتم رفع الملف إليه
                };

                // رفع الملف
                var request = service.Files.Create(fileMetadata, fileStream, file.ContentType);
                request.Fields = "id";
                var uploadedFile = await request.UploadAsync();

                if (uploadedFile.Exception != null)
                {
                    Console.WriteLine($"Error uploading file: {uploadedFile.Exception.Message}");
                    return null;
                }

                Console.WriteLine($"File uploaded successfully! File ID: {request.ResponseBody.Id}");
                return request.ResponseBody.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        public static async Task<string> GetFileAsBase64Async(string fileId)
        {
            try
            {
                GoogleCredential credential;
                using (var stream = new FileStream(CredentialsPath, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
                }

                using var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                // طلب قراءة الملف من Google Drive
                var request = service.Files.Get(fileId);
                using var streamOut = new MemoryStream();
                await request.DownloadAsync(streamOut);

                // تحويل الملف إلى Base64
                string base64String = Convert.ToBase64String(streamOut.ToArray());

                Console.WriteLine($"File converted to Base64 successfully!");
                return base64String;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return null;
            }
        }
        public static async Task<bool> DeleteImageAsync(string fileId)
        {
            try
            {
                using var service = GetDriveService();
                await service.Files.Delete(fileId).ExecuteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file: {ex.Message}");
                return false;
            }
        }

    }
}



