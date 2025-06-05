using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Review.Commands.Models
{
    public class AddOrUpdateReviewCommand : IRequest<Response<string>>
    {
        public Guid UserId { get; set; }
        public int ProductId { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
    }
}
