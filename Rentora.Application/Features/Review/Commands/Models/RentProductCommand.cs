using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Review.Commands.Models
{
    public class AddReviewCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
