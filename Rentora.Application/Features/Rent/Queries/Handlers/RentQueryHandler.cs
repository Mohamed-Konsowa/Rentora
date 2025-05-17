using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Rent.Queries.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Rent.Queries.Handlers
{
    internal class RentCommandHandler : IRequestHandler<GetUserRents, Response<List<int>>>
    {
        private readonly IRentService _rentService;

        public RentCommandHandler(IRentService rentService)
        {
            _rentService = rentService;
        }
        public async Task<Response<List<int>>> Handle(GetUserRents request, CancellationToken cancellationToken)
        {
            var result = await _rentService.GetUserRentsAsync(request.UserId);
            if(result == null) return ResponseHandler.NotFound<List<int>>("User not found!");
            return ResponseHandler.Success(result);
            throw new NotImplementedException();
        }
    }
}
