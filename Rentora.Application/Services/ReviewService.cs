using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rentora.Application.Base;
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
            var result = await _unitOfWork.reviews.AddAsync(review);
            await _unitOfWork.SaveChangesAsync();
            return result is not null;
        }

        public async Task<(IReadOnlyCollection<GetProductReviewsDTO>, int)> GetProductReviewsPaginatedAsync(int productId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _unitOfWork.reviews.PaginateAsync
            (
            pageNumber,
            pageSize,
                r => _mapper.Map<GetProductReviewsDTO>(r),
                r => r.ProductId == productId,
                null,
                null,
                cancellationToken
            );
        }
        public async Task<(int Count, float Rate)> GetProductRateAsync(int productId)
        {
            var Count = await _unitOfWork.reviews
                .GetProductReviews(productId)
                .CountAsync();

            var RateAvg = await _unitOfWork.reviews
                .GetProductReviews(productId)
                .AverageAsync(r => r.Rating);

            return (Count, RateAvg);
        }

        public Task<Review> GetReviewAsync(string userId, int productId)
        {
            return _unitOfWork.reviews.GetReviewAsync(userId, productId);
        }

        public async Task<bool> IsUserReviewedBeforeAsync(string userId, int productId)
        {
            return await _unitOfWork.reviews.IsUserReviewedBeforeAsync(userId, productId);
        }

        public async Task<bool> UpdateReviewAsync(AddReviewDTO reviewDTO)
        {
            var re = await _unitOfWork.reviews.GetReviewAsync(reviewDTO.ReviewerId, reviewDTO.ProductId);
            re.Rating = reviewDTO.Rating;
            re.Comment = reviewDTO.Comment;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var r = await _unitOfWork.reviews.GetByIdAsync(reviewId);
            if (r is null) return false;
            _unitOfWork.reviews.Delete(reviewId);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
