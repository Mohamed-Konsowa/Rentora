using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Cart.Queries.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Cart.Queries.Handlers
{
    public class CartQueryHandler : IRequestHandler<GetUserCartItemsPaginatedQuery, Response<List<int>>>
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;
        public CartQueryHandler(ICartService cartService, IUserService userService)
        {
            _cartService = cartService;
            _userService = userService;
        }
        public async Task<Response<List<int>>> Handle(GetUserCartItemsPaginatedQuery request, CancellationToken cancellationToken)
        {
            if (await _userService.GetUserByIdAsync(request.UserId.ToString()) is null)
                return ResponseHandler.NotFound<List<int>>("User not found!");

            request.PageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            request.PageSize = request.PageSize <= 0 ? 10 : request.PageSize;
            var Ids = await _cartService.GetUserCartItemsPaginatedAsync
            (
                 request.UserId.ToString(),
                 (int)request.PageNumber,
                 (int)request.PageSize,
                 cancellationToken
            );

            var response = ResponseHandler.Success(Ids.Item1.ToList());
            response.Meta = new PaginatedMeta
            {
                CurrentPage = (int)request.PageNumber,
                Succeeded = true,
                PageSize = (int)request.PageSize,
                TotalPages = (int)Math.Ceiling((float)Ids.Item2 / (int)request.PageSize),
                TotalCount = Ids.Item2
            };
            return response;
        }
    }
}
