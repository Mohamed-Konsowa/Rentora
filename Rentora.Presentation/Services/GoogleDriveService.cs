using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Rentora.Presentation.Services
{
    class GoogleDriveService2
    {
        private static readonly string[] Scopes = { DriveService.Scope.DriveFile };
        private const string ApplicationName = "Rentora";
        private const string CredentialsPath = "Resourses/rentora-453721-0bd2cc9b6341.json"; 

        public static async Task<string> UploadFileAsync(string filePath, string folderId = "1kBrUx090uTdeHUx2ZUzDGTwTO0Ebx8Dh")
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

                var fileMetadata = new Google.Apis.Drive.v3.Data.File
                {
                    Name = Path.GetFileName(filePath),
                    Parents = new[] { folderId }
                };

                using var fileStream = new FileStream(filePath, FileMode.Open);
                var request = service.Files.Create(fileMetadata, fileStream, "image/jpeg");
                request.Fields = "id";
                var result = await request.UploadAsync();

                if (result.Status == UploadStatus.Failed)
                    throw new Exception("Upload failed");

                return $"https://drive.google.com/uc?id={request.ResponseBody.Id}"; // رابط الصورة
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading file: {ex.Message}");
                return null;
            }
        }
    }
    class GoogleDriveService
    {
        private static readonly string[] Scopes = { DriveService.Scope.DriveFile };
        private const string ApplicationName = "Rentora";
        private const string CredentialsPath = "wwwroot/Resourses/rentora-453721-0bd2cc9b6341.json"; // تأكد أن الملف في هذا المسار

        public static async Task<string> UploadFileToDriveAsync(IFormFile file, string folderId = "1kBrUx090uTdeHUx2ZUzDGTwTO0Ebx8Dh")
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
    }
}



