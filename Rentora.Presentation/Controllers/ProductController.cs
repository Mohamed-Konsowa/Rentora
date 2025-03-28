using Microsoft.AspNetCore.Mvc;
using Rentora.Presentation.Base;
using Rentora.Domain.AppMetaData;
using Rentora.Application.Features.Product.Queries.Models;
using Rentora.Application.Features.Product.Commands.Models;

namespace Rentora.Presentation.Controllers
{
    public class ProductController : AppControllerBase
    {
        [HttpGet(Router.Product.GetAll)]
        public async Task<IActionResult> GetProducts()
        {
            return NewResult(await _mediator.Send(new GetProductsQuery()));
        }

        [HttpGet(Router.Product.GetPById)]
        public async Task<IActionResult> GetProductAsync([FromRoute]int productId)
        {
            return NewResult(await _mediator.Send(new GetProductByIdQuery { ProductId = productId }));
        }

        //[Authorize]
        [HttpPost(Router.Product.Add)]
        public async Task<IActionResult> AddProductAsync(AddProductCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        [HttpPut]
        [Route(Router.Product.Update)]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        //[Authorize]
        [HttpDelete(Router.Product.Delete)]
        public async Task<IActionResult> DeleteProductAsync(DeleteProductCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        [HttpPost(Router.Product.AddImage)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddProdectImage(AddImageCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        [HttpGet]
        [Route(Router.Product.GetImages)]
        public async Task<IActionResult> GetProductImagesById([FromRoute]int productId)
        {
            return NewResult(await _mediator.Send(new GetProductImagesQuery() { ProductId = productId}));
        }

        [HttpDelete]
        [Route(Router.Product.DeleteImage)]
        public async Task<IActionResult> DeleteImageById([FromRoute]int imageId)
        {
            return NewResult(await _mediator.Send(new DeleteImageCommand { ImageId = imageId}));
        }
    }
}
