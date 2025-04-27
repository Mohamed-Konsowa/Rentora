
using AutoMapper;
using Rentora.Application.DTOs.Review;
using Rentora.Application.Features.Review.Commands.Models;

namespace Rentora.Application.Mapping.Review
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            Map();
        }
        private void Map()
        {
            CreateMap<AddReviewCommand, AddReviewDTO>()
                .ForMember(r => r.ReviewerId, opc => opc.MapFrom(src => src.UserId));

            CreateMap<Domain.Models.Review, GetProductReviewsDTO>()
                .ForMember(r => r.ReviewerId, opc => opc.MapFrom(src => src.ApplicationUserId))
                .ForMember(r => r.ProductReviewId, opc => opc.MapFrom(src => src.ReviewId));
        }
    }
}
