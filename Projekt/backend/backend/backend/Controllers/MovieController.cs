using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.Data.Enums;
using backend.Data.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace backend.Controllers
{
    [Authorize(Roles = "User, Admin")]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addMovie/{name}/{description}")]
        public async Task<IActionResult> AddMovie(string name, string description, [FromForm] ImageModel imageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieModel = new MovieModel()
            {
                Name = name,
                Description = description,
                Logo = imageModel.Image
            };

            var result = await _movieService.AddMovie(movieModel);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("updateMovie/{id}/{name}/{description}")]
        public async Task<IActionResult> UpdateMovie(int id, string name, string description, ImageModel imageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var movieModel = new MovieModel()
            {
                Name = name,
                Description = description,
                Logo = imageModel.Image
            };

            var result = await _movieService.UpdateMovie(id, movieModel);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteMovie/{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var result = await _movieService.DeleteMovie(id);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("getMovie/{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var result = await _movieService.GetMovie(id, user);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("getMovies")]
        public async Task<IActionResult> GetMovies()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var result = await _movieService.GetMovies();

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("addReview")]
        public async Task<IActionResult> AddReview([FromBody]ReviewModel reviewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _movieService.AddReview(reviewModel);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("addFavouriteMovie/{movieId}")]
        public async Task<IActionResult> AddFavouriteMovie(int movieId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var result = await _movieService.AddFavouriteMovie(user, movieId);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("deleteFavouriteMovie/{movieId}")]
        public async Task<IActionResult> DeleteFavouriteMovie(int movieId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var result = await _movieService.DeleteFavouriteMovie(user, movieId);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("getFavouriteMovies")]
        public async Task<IActionResult> GetFavouriteMovies()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var result = await _movieService.GetFavouriteMovies(user);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
