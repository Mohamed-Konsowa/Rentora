using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Review.Commands.Models
{
    public class AddReviewCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
    }
}
