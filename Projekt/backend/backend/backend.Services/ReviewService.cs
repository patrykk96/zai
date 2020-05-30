using backend.Data.DbModels;
using backend.Data.Dto;
using backend.Data.Enums;
using backend.Data.Models;
using backend.Repository;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Movie> _repo;
        private readonly IRepository<Review> _reviewRepo;
        private readonly UserManager<User> _userManager;

        public ReviewService(IRepository<Movie> repo, IRepository<Review> reviewRepo, UserManager<User> userManager)
        {
            _repo = repo;
            _reviewRepo = reviewRepo;
            _userManager = userManager;
        }


        public async Task<ResultDto<BaseDto>> AddReview(ReviewModel reviewModel, ClaimsPrincipal user)
        {
            var result = new ResultDto<BaseDto>()
            {
                Error = null
            };

            //sprawdzam czy uzytkownik istnieje
            var userIdentity = await _userManager.FindByEmailAsync(user.Identity.Name);
            var userId = userIdentity.Id;

            if (userId == null)
            {
                result.Error = "Autor poradnika nie został odnaleziony";
                return result;
            }

            //sprawdzam czy film istnieje
            bool exists = await _repo.Exists(x => x.Id == reviewModel.MovieId);

            if (!exists)
            {
                result.Error = "Film o takim id nie istnieje";
                return result;
            }

            //sprawdzam czy recenzja danego filmu istnieje
            exists = await _reviewRepo.Exists(x => x.UserId == userId && x.MovieId == reviewModel.MovieId);

            if (exists)
            {
                result.Error = "Recenzja została już stworzona";
                return result;
            }

            //tworze recenzje i zapisuje w bazie danych
            var review = new Review()
            {
                Score = reviewModel.Score,
                Content = reviewModel.Content,
                UserId = userId,
                MovieId = reviewModel.MovieId,
                Created = DateTime.Now
            };

            try
            {
                await _reviewRepo.Add(review);
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        //usuwanie recenzji
        public async Task<ResultDto<BaseDto>> DeleteReview(int reviewid, ClaimsPrincipal user)
        {
            var result = new ResultDto<BaseDto>()
            {
                Error = null
            };

            //sprawdzam czy uzytkownik jest wlascicielem recenzji
            var userIdentity = await _userManager.FindByEmailAsync(user.Identity.Name);
            var userId = userIdentity.Id;

            var review = await _reviewRepo.GetEntity(x => x.Id == reviewid);

            if (review == null)
            {
                result.Error = "Nie odnaleziono podanej recenzji";
                return result;
            }

            if (review.UserId != userId)
            {
                result.Error = "Tylko autor może usunąć swoją recenzje";
                return result;
            }

            try
            {
                await _reviewRepo.Delete(review);
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }


        public async Task<ResultDto<ReviewDto>> GetReview(int reviewid, ClaimsPrincipal user)
        {
            var result = new ResultDto<ReviewDto>()
            {
                Error = null
            };

            //probuje uzyskac wskazana recenzje
            var review = await _reviewRepo.GetEntity(x => x.Id == reviewid);

            if (review == null)
            {
                result.Error = "Nie odnaleziono recenzji";
                return result;
            }

            //pobieram nazwe uzytkownika
            var reviewOwner = await _userManager.FindByIdAsync(review.UserId);
            var reviewOwnerName = reviewOwner.UserName;

            //tworze obiekt z recenzja i zwracam go
            var reviewToSend = new ReviewDto()
            {
                ReviewId = review.Id,
                Content = review.Content,
                Rating = review.Score,
                Author = reviewOwnerName,
                MovieId = review.MovieId,
            };

            result.SuccessResult = reviewToSend;

            return result;
        }

        public async Task<ResultDto<ListReviewDto>> GetReviews(int movieid, ClaimsPrincipal user)
        {

            var result = new ResultDto<ListReviewDto>()
            {
                Error = null
            };

            var reviews = await _reviewRepo.GetBy(x => x.MovieId == movieid);

            List<ReviewDto> reviewsToSend = new List<ReviewDto>();

            //tworze liste recenzji do zwrocenia
            foreach (var review in reviews)
            {
                //pobieram nazwe uzytkownika
                var reviewOwner = await _userManager.FindByIdAsync(review.UserId);
                var reviewOwnerName = reviewOwner.UserName;

                var m = new ReviewDto()
                {
                    ReviewId = review.Id,
                    Content = review.Content,
                    Rating = review.Score,
                    Author = reviewOwnerName,
                    MovieId = review.MovieId
                };

                reviewsToSend.Add(m);
            }

            var reviewList = new ListReviewDto()
            {
                List = reviewsToSend
            };

            result.SuccessResult = reviewList;

            return result;

        }

        public async Task<ResultDto<BaseDto>> UpdateReview(ReviewModel reviewModel, ClaimsPrincipal user)
        {

            var result = new ResultDto<BaseDto>()
            {
                Error = null
            };

            //sprawdzam czy uzytkownik istnieje
            var userIdentity = await _userManager.FindByEmailAsync(user.Identity.Name);
            var userId = userIdentity.Id;

            if (userId == null)
            {
                result.Error = "Autor poradnika nie został odnaleziony";
                return result;
            }

            //sprawdzam czy film istnieje
            bool exists = await _repo.Exists(x => x.Id == reviewModel.MovieId);

            if (!exists)
            {
                result.Error = "Film o takim id nie istnieje";
                return result;
            }

            //sprawdzam czy recenzja danego filmu istnieje
            var review = await _reviewRepo.GetEntity(x => x.UserId == userId && x.MovieId == reviewModel.MovieId);

            if (review == null)
            {
                result.Error = "Recenzja nie została stworzona";
                return result;
            }

            //zmiana recenzji

            if (reviewModel.Content != "null")
            {
                review.Content = reviewModel.Content;
            }
            
            review.Score = reviewModel.Score;
            review.Updated = DateTime.Now;

            try
            {
                await _reviewRepo.Update(review);
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }
    }
}
