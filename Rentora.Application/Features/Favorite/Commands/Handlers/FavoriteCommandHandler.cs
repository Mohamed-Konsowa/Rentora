using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Favorite.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Favorite.Commands.Handlers
{
    public class CartCommandHandler : IRequestHandler<AddToFavoriteCommand, Response<string>>
                                    , IRequestHandler<RemoveFromFavoriteCommand, Response<string>>
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public CartCommandHandler(IFavoriteService favoriteService, IUserService userService, IProductService productService)
        {
            _favoriteService = favoriteService;
            _userService = userService;
            _productService = productService;
        }
        public async Task<Response<string>> Handle(AddToFavoriteCommand request, CancellationToken cancellationToken)
        {
            if (await _userService.GetUserByIdAsync(request.UserId.ToString()) is null)
                return ResponseHandler.NotFound<string>("User not found!");

            if (await _productService.GetProductByIdAsync((int)request.ProductId) is null)
                return ResponseHandler.NotFound<string>("Product not found!");

            var result = await _favoriteService.AddInFavoriteAsync(request.UserId.ToString(), (int)request.ProductId);
            if (result) return ResponseHandler.Success("", "Product added to favorites successfully.");
            return ResponseHandler.BadRequest<string>("Failed to add Product!");
        }

        public async Task<Response<string>> Handle(RemoveFromFavoriteCommand request, CancellationToken cancellationToken)
        {
            if (await _userService.GetUserByIdAsync(request.UserId.ToString()) is null)
                return ResponseHandler.NotFound<string>("User not found!");

            if (await _productService.GetProductByIdAsync((int)request.ProductId) is null)
                return ResponseHandler.NotFound<string>("Product not found!");

            var result = await _favoriteService.RemoveFromFavoriteAsync(request.UserId.ToString(), (int)request.ProductId);
            if (result) return ResponseHandler.Success("", "Product removed successfully.");
            return ResponseHandler.BadRequest<string>("Failed to remove Product!");
        }
    }
}
