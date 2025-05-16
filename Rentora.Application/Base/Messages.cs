namespace Rentora.Application.Base
{
    public static class Messages
    {
        // General Success Messages
        public const string Success = "Operation completed successfully.";
        public const string Created = "Resource created successfully.";
        public const string Deleted = "Resource deleted successfully.";

        // Error Messages
        public const string BadRequest = "The request was invalid.";
        public const string Unauthorized = "You are not authorized to perform this action.";
        public const string NotFound = "The requested resource was not found.";
        public const string Unprocessable = "The request could not be processed.";
        public const string InternalServerError = "An unexpected error occurred. Please try again later.";

        // Validation Messages
        public const string ValidationFailed = "One or more validation errors occurred.";
    }
}
