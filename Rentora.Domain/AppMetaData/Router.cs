﻿
namespace Rentora.Domain.AppMetaData
{
    public class Router
    {
        private const string SignleRoute = "/{id}";

        private const string Root = "api";
        private const string Rule = Root + "/";

        public static class Account
        {
            public const string Prefix = Rule + "Account/";
            public const string GetAll = Prefix + "getAllUsers";
            public const string GetById = Prefix + "getUserById/{userId}";
            public const string CheckIfEmailExists = Prefix + "checkIfEmailExists/{email}";
            public const string CheckIfUserNameExists = Prefix + "checkIfUserNameExists/{userName}";
            public const string Register = Prefix + "register";
            public const string UpdateProfile = Prefix + "updateUser";
            public const string Login = Prefix + "login"; 
            public const string ResetPassword = Prefix + "resetPassword";
            public const string UpdateProfileImage = Prefix + "updateProfileImage";
            public const string Role = Prefix + "addrole"; 
        }
        public static class Cart
        {
            public const string Prefix = Rule + "Cart/";
            public const string GetUserCart = Prefix + "getUserCartItemsPaginated/{userId}";
            public const string Add = Prefix + "addProductToCart";
            public const string Remove = Prefix + "removeFromCart";
        }
        public static class Email
        {
            public const string Prefix = Rule + "Email/";
            public const string Send = Prefix + "sendEmail";
            public const string SendOTP = Prefix + "sendOTP";
            public const string VerifyOTP = Prefix + "verifyOTP";
            public const string SendResetPasswordToken = Prefix + "sendResetPasswordToken";
        }
        public static class Favorite
        {
            public const string Prefix = Rule + "Favorite/";
            public const string GetUserFav = Prefix + "getUserFavoriteItemsPaginated/{userId}";
            public const string Add = Prefix + "addProductToFavorite";
            public const string Remove = Prefix + "removeFromFavorite";
        }
        public static class Product
        {
            public const string Prefix = Rule + "Product/";
            public const string GetAll = Prefix + "getProducts";
            public const string GetPaginated = Prefix + "getProductsPaginated";
            public const string GetPById = Prefix + "getProductById/{productId}";
            public const string Add = Prefix + "addProduct"; 
            public const string Update = Prefix + "updateProduct";
            public const string Delete = Prefix + "deleteProduct/{productId}";
            public const string AddImage = Prefix + "addImage"; 
            public const string GetImages = Prefix + "getProductImagesById/{productId}"; 
            public const string DeleteImage = Prefix + "DeleteProductImageById/{imageId}"; 
        }
        public static class Rent
        {
            public const string Prefix = Rule + "Rent/";
            public const string GetUserRents = Prefix + "getUserRentsPaginated/{userId}";
            public const string RentProduct = Prefix + "rentProduct";
            public const string ReturnProduct = Prefix + "returnProduct/{productId}";
        }

        public static class Review 
        {
            public const string Prefix = Rule + "Review/";
            public const string AddOrUpdateReview = Prefix + "addOrUpdateReview";
            public const string GetProductReviews = Prefix + "getProductReviewsPaginated/{productId}";
            public const string GetProductRate = Prefix + "getProductRate/{productId}";
            public const string DeleteReview = Prefix + "deleteReview/{reviewId}";
        }
    }
}
