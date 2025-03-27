
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
            public const string Login = Prefix + "login"; 
            public const string Role = Prefix + "addrole"; 
        }
        public static class Cart
        {
            public const string Prefix = Rule + "Cart/";
            public const string GetUserFav = Prefix + "getUserCartItems/{userId}";
            public const string Add = Prefix + "addProductToCart";
            public const string Remove = Prefix + "removeFromCart";
        }
        public static class Email
        {
            public const string Prefix = Rule + "Email/";
            public const string Send = Prefix + "sendEmail";
            public const string SendOTP = Prefix + "sendOTP";
            public const string VerifyOTP = Prefix + "verifyOTP";
        }
        public static class Favorite
        {
            public const string Prefix = Rule + "Favorite/";
            public const string GetUserFav = Prefix + "getUserFavoriteItems/{userId}";
            public const string Add = Prefix + "addProductToFavorite";
            public const string Remove = Prefix + "removeFromFavorite";
        }
    }
}
