using Microsoft.AspNetCore.Mvc;
using Rentora.Application.DTOs.Rental;
using Rentora.Application.Features.Rent.Commands.Models;
using Rentora.Application.Features.Rent.Queries.Models;
using Rentora.Domain.AppMetaData;
using Rentora.Presentation.Base;

namespace Rentora.Presentation.Controllers
{
    public class RentController : AppControllerBase
    {
        [HttpGet]
        [Route(Router.Rent.GetUserRents)]
        public async Task<IActionResult> GetUserRents([FromRoute]string  userId)
        {
            return NewResult(await _mediator.Send(new GetUserRents { UserId = userId}));
        }
        [HttpPost]
        [Route(Router.Rent.RentProduct)]
        public async Task<IActionResult> RentProductAsync(RentProductDTO rent)
        {
            return NewResult(await _mediator.Send(new RentProductCommand { DTO = rent}));
        }
    }
}
