using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentora.Application.DTOs.Rental;
using Rentora.Application.Features.Rent.Commands.Models;
using Rentora.Application.Features.Rent.Queries.Models;
using Rentora.Domain.AppMetaData;
using Rentora.Presentation.Base;

namespace Rentora.Presentation.Controllers
{
    [Authorize]
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

        /// <summary>
        /// Return a product to the owner.
        /// </summary>
        [HttpPost]
        [Route(Router.Rent.ReturnProduct)]
        public async Task<IActionResult> ReturnProduct([FromRoute(Name = "productId")] int ProductId)
        {
            return NewResult(await _mediator.Send(new ReturnProductCommand { ProductId = ProductId}));
        }
    }
}
