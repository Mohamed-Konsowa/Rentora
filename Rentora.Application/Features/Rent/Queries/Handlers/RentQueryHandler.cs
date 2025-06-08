using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Rent.Queries.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Rent.Queries.Handlers
{
    internal class RentCommandHandler : IRequestHandler<GetUserRentsPaginatedQuery, Response<List<int>>>
    {
        private readonly IRentService _rentService;

        public RentCommandHandler(IRentService rentService)
        {
            _rentService = rentService;
        }
        public async Task<Response<List<int>>> Handle(GetUserRentsPaginatedQuery request, CancellationToken cancellationToken)
        {
            request.PageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            request.PageSize = request.PageSize <= 0 ? 10 : request.PageSize;
            var Ids = await _rentService.GetUserRentsPaginatedAsync
            (
                 request.UserId,
                 request.PageNumber,
                 request.PageSize,
                 cancellationToken
            );

            var response = ResponseHandler.Success(Ids.Item1.ToList());
            response.Meta = new PaginatedMeta
            {
                CurrentPage = request.PageNumber,
                Succeeded = true,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling((float)Ids.Item2 / request.PageSize),
                TotalCount = Ids.Item2
            };
            return response;
        }
    }
}
