using backend.Data.Dto;
using backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services.Interfaces
{
    public interface IReviewService
    {
        Task<ResultDto<BaseDto>> AddReview(ReviewModel reviewModel, ClaimsPrincipal user);
        Task<ResultDto<BaseDto>> UpdateReview(ReviewModel reviewModel, ClaimsPrincipal user);
        Task<ResultDto<BaseDto>> DeleteReview(int id, ClaimsPrincipal user);
        Task<ResultDto<ReviewDto>> GetReview(int reviewid, ClaimsPrincipal user);
        Task<ResultDto<ListReviewDto>> GetReviews(int movieid, ClaimsPrincipal user);
    }
}
