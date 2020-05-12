using backend.Data.Dto;
using backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services.Interfaces
{
    public interface IMovieService
    {
        Task<ResultDto<BaseDto>> AddMovie(MovieModel movieModel);
        Task<ResultDto<BaseDto>> UpdateMovie(int id, MovieModel movieModel);
        Task<ResultDto<BaseDto>> DeleteMovie(int id);
        Task<ResultDto<MovieDto>> GetMovie(int id);
        Task<ResultDto<ListMovieDto>> GetMovies(int id);
        Task<ResultDto<BaseDto>> AddReview(ReviewModel reviewModel);
        //Task<ResultDto<ReviewDto>> GetReview(int userId, int gameId);
    }
}