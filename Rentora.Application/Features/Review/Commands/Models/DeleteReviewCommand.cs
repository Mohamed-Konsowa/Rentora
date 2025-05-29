using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Review.Commands.Models
{
    public class DeleteReviewCommand : IRequest<Response<string>>
    {
        public int reviewId { get; set; }
    }
}
