using Microsoft.AspNetCore.Mvc;
using Rentora.Presentation.Base;
using Rentora.Domain.AppMetaData;
using Rentora.Application.Features.Product.Queries.Models;
using Rentora.Application.Features.Product.Commands.Models;
using Microsoft.AspNetCore.Authorization;

namespace Rentora.Presentation.Controllers
{
    [Authorize]
    public class ProductController : AppControllerBase
    {
        /// <summary>
        /// Gets all available products.
        /// </summary>
        [HttpGet(Router.Product.GetAll)]
        public async Task<IActionResult> GetProducts()
        {
            return NewResult(await _mediator.Send(new GetProductsQuery()));
        }

        /// <summary>
        /// Search products with pagination.
        /// </summary>
        [HttpGet(Router.Product.GetPaginated)]
        public async Task<IActionResult> GetProductsPaginated([FromQuery]GetProductsPaginatedQuery request)
        {
            return NewResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Gets a product by its ID.
        /// </summary>
        [HttpGet(Router.Product.GetPById)]
        public async Task<IActionResult> GetProductAsync([FromRoute]int productId)
        {
            return NewResult(await _mediator.Send(new GetProductByIdQuery { ProductId = productId }));
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        //[Authorize]
        [HttpPost(Router.Product.Add)]
        public async Task<IActionResult> AddProductAsync(AddProductCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        [HttpPut]
        [Route(Router.Product.Update)]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        //[Authorize]
        [HttpDelete(Router.Product.Delete)]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int productId)
        {
            return NewResult(await _mediator.Send(new DeleteProductCommand { ProductId = productId }));
        }

        /// <summary>
        /// Adds an image to a product.
        /// </summary>
        [HttpPost(Router.Product.AddImage)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddProdectImage(AddImageCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Gets all images for a product.
        /// </summary>
        [HttpGet]
        [Route(Router.Product.GetImages)]
        public async Task<IActionResult> GetProductImagesById([FromRoute]int productId)
        {
            return NewResult(await _mediator.Send(new GetProductImagesQuery() { ProductId = productId}));
        }

        /// <summary>
        /// Deletes an image by its ID.
        /// </summary>
        [HttpDelete]
        [Route(Router.Product.DeleteImage)]
        public async Task<IActionResult> DeleteImageById([FromRoute]int imageId)
        {
            return NewResult(await _mediator.Send(new DeleteImageCommand { ImageId = imageId}));
        }
    }
}
