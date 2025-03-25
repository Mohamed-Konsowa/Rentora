
namespace Rentora.Domain.AppMetaData
{
    public class Router
    {
        private const string SignleRoute = "/{id}";

        private const string Root = "api";
        private const string Rule = Root + "/";

        public static class Favorite
        {
            public const string Prefix = Rule + "Favorite";
            public const string GetUserFav = Prefix + "/getUserFavoriteItems/{userId}";
            public const string Add = Prefix + "/addProductToFavorite";
            public const string Remove = Prefix + "/removeFromFavorite";
        }
    }
}
