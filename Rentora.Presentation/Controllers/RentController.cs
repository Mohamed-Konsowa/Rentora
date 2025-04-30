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
        /// <summary>
        /// Gets all rented items by a user.
        /// </summary>
        [HttpGet]
        [Route(Router.Rent.GetUserRents)]
        public async Task<IActionResult> GetUserRents([FromRoute]string  userId)
        {
            return NewResult(await _mediator.Send(new GetUserRents { UserId = userId}));
        }

        /// <summary>
        /// Rents a product to a user.
        /// </summary>
        [HttpPost]
        [Route(Router.Rent.RentProduct)]
        public async Task<IActionResult> RentProductAsync(RentProductDTO rent)
        {
            return NewResult(await _mediator.Send(new RentProductCommand { DTO = rent}));
        }
    }
}
