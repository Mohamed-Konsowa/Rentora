using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Product.Commands.Models
{
    public class DeleteImageCommand : IRequest<Response<string>>
    {
        public int? ImageId {  get; set; }
    }
}
