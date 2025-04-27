using AutoMapper;
using Rentora.Application.DTOs.Review;
using Rentora.Application.IRepositories;
using Rentora.Application.IServices;
using Rentora.Domain.Models;

namespace Rentora.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> AddReviewAsync(AddReviewDTO reviewDTO)
        {
            var review = new Review()
            {
                ApplicationUserId = reviewDTO.ReviewerId,
                ProductId = reviewDTO.ProductId,
                Rating = reviewDTO.Rating,
                Comment = reviewDTO.Comment
            };
            var result = await _unitOfWork.reviews.Add(review);
            await _unitOfWork.Save();
            return result is not null;
        }

        public async Task<List<GetProductReviewsDTO>> GetProductReviewsAsync(int productId)
        {
            var list = _unitOfWork.reviews.GetProductReviews(productId).ToList();
            var result = new List<GetProductReviewsDTO>();
            list.ForEach(review => { result.Add(_mapper.Map<GetProductReviewsDTO>(review)); });
            return result;
        }

        public bool IsUserReviewedBefore(string userId, int productId)
        {
            return _unitOfWork.reviews.IsUserReviewedBefore(userId, productId);
        }
    }
}
