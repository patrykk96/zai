using backend.Data.Dto;
using backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services.Interfaces
{
    public interface IMovieService
    {
        Task<ResultDto<BaseDto>> AddMovie(MovieModel movieModel);
        Task<ResultDto<BaseDto>> UpdateMovie(int id, MovieModel movieModel);
        Task<ResultDto<BaseDto>> DeleteMovie(int id);
        Task<ResultDto<MovieDto>> GetMovie(int id, ClaimsPrincipal user);
        Task<ResultDto<ListMovieDto>> GetMovies();
        Task<ResultDto<BaseDto>> AddFavouriteMovie(ClaimsPrincipal user, int movieId);
        Task<ResultDto<BaseDto>> DeleteFavouriteMovie(ClaimsPrincipal user, int movieId);
        Task<ResultDto<ListMovieDto>> GetFavouriteMovies(ClaimsPrincipal user);
    }
}