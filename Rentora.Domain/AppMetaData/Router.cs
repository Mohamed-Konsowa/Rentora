
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
