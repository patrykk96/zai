using backend.Data.DbModels;
using backend.Data.Dto;
using backend.Data.Enums;
using backend.Data.Models;
using backend.Repository;
using backend.Services.Helpers;
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
        private readonly IRepository<Movie> _movieRepo;
        private readonly IRepository<Review> _reviewRepo;
        private readonly UserManager<User> _userManager;

        public ReviewService(IRepository<Movie> repo, IRepository<Review> reviewRepo, UserManager<User> userManager)
        {
            _movieRepo = repo;
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
            string userId = await _userManager.GetId(user);

            if (string.IsNullOrEmpty(userId))
            {
                result.Error = "Autor poradnika nie został odnaleziony";
                return result;
            }

            //sprawdzam czy film istnieje
            bool exists = await _movieRepo.Exists(x => x.Id == reviewModel.MovieId);

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
            string userId = await _userManager.GetId(user);

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
            var loggedInUserId = await _userManager.GetId(user);

            //pobieram nazwe filmu
            var movie = await _movieRepo.GetEntity(x => x.Id == review.MovieId);

            if (movie == null)
            {
                result.Error = "Nie odnaleziono filmu";
                return result;
            }

            //tworze obiekt z recenzja i zwracam go
            var reviewToSend = new ReviewDto()
            {
                ReviewId = review.Id,
                Content = review.Content,
                Rating = review.Score,
                Author = review.User.UserName,
                IsAuthor = review.UserId == loggedInUserId ? true : false,
                MovieId = review.MovieId,
                MovieName = movie.Name
            };

            result.SuccessResult = reviewToSend;

            return result;
        }

        public async Task<ResultDto<ListReviewDto>> GetReviews(int movieId, ClaimsPrincipal user)
        {

            var result = new ResultDto<ListReviewDto>()
            {
                Error = null
            };

            List<Review> reviews = new List<Review>();

            var userId = await _userManager.GetId(user);

            if (movieId != 0)
            {
                reviews = await _reviewRepo.GetBy(x => x.MovieId == movieId);
            }
            else
            {
                reviews = await _reviewRepo.GetBy(x => x.UserId == userId);
            }
            

            //pobieram nazwe filmu
            var movie = await _movieRepo.GetEntity(x => x.Id == movieId);

            if (movie == null && movieId != 0)
            {
                result.Error = "Nie odnaleziono filmu";
                return result;
            }

            List<ReviewDto> reviewsToSend = new List<ReviewDto>();

            //tworze liste recenzji do zwrocenia
            foreach (var review in reviews)
            {
                //pobieram nazwe uzytkownika
                var reviewContent = review.Content.Substring(0, Math.Min(review.Content.Length, 50));

                if (reviewContent.Length == 50)
                {
                    reviewContent += "...";
                }

                string movieName;

                if (movieId == 0)
                {
                    var movieFromRepo = await _movieRepo.GetEntity(x => x.Id == review.MovieId);
                    movieName = movieFromRepo.Name;
                }
                else
                {
                    movieName = movie.Name;
                }

                var m = new ReviewDto()
                {
                    ReviewId = review.Id,
                    Content = reviewContent,
                    Rating = review.Score,
                    Author = review.User.UserName,
                    MovieId = review.MovieId,
                    MovieName = movieName
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

        public async Task<ResultDto<BaseDto>> UpdateReview(int reviewid, ReviewModel reviewModel, ClaimsPrincipal user)
        {

            var result = new ResultDto<BaseDto>()
            {
                Error = null
            };

            //sprawdzam czy uzytkownik istnieje
            string userId = await _userManager.GetId(user);

            if (string.IsNullOrEmpty(userId))
            {
                result.Error = "Autor poradnika nie został odnaleziony";
                return result;
            }

            //sprawdzam czy film istnieje
            bool exists = await _movieRepo.Exists(x => x.Id == reviewModel.MovieId);

            if (!exists)
            {
                result.Error = "Film o takim id nie istnieje";
                return result;
            }

            //sprawdzam czy recenzja danego filmu istnieje
            var review = await _reviewRepo.GetEntity(x => x.Id == reviewid);

            if (review == null)
            {
                result.Error = "Recenzja nie została stworzona";
                return result;
            }

            //zmiana recenzji pod warunkiem, ze autor ja zmienia
            if (review.UserId == userId)
            {
                if (reviewModel.Content != "null")
                {
                    review.Content = reviewModel.Content;
                }

                review.Score = reviewModel.Score;
                review.Updated = DateTime.Now;
            }
            else {
                result.Error = "Uzytkownik nie jest wlascicielem recenzji";
                return result;
            }

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
