using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Rent.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Rent.Commands.Handlers
{
    internal class RentCommandHandler : ResponseHandler
                                    , IRequestHandler<RentProductCommand, Response<string>>
    {
        private readonly IRentService _rentService;

        public RentCommandHandler(IRentService rentService)
        {
            _rentService = rentService;
        }

        public async Task<Response<string>> Handle(RentProductCommand request, CancellationToken cancellationToken)
        {
            var result = await  _rentService.RentProduct(request.DTO);
            if(result) return Success("");
            return BadRequest<string>("");
        }
    }
}
