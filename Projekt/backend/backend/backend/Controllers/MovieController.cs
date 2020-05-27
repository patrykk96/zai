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
    [Authorize]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        //do czesci metod w tym kontrolerze dostep ma tylko administrator, w tym celu sprawdzana jest rola w przeslanym tokenie
        [HttpPost("addMovie/{name}/{description}")]
        public async Task<IActionResult> AddMovie(string name, string description, [FromForm] ImageModel imageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //var role = int.Parse(User.FindFirst(ClaimTypes.Role)?.Value);

            //if (role != (int)UserRole.Admin)
            //{
            //    return Unauthorized();
            //}

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

        [HttpPatch("updateMovie/{id}/{name}/{description}")]
        public async Task<IActionResult> UpdateMovie(int id, string name, string description, ImageModel imageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //var role = int.Parse(User.FindFirst(ClaimTypes.Role)?.Value);

            //if (role != (int)UserRoles.admin)
            //{
            //    return Unauthorized();
            //}

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

        [HttpDelete("deleteMovie/{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //var role = int.Parse(User.FindFirst(ClaimTypes.Role)?.Value);

            //if (role != (int)UserRoles.admin)
            //{
            //    return Unauthorized();
            //}

            var result = await _movieService.DeleteMovie(id);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("getMovie/{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _movieService.GetMovie(id);

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

            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null) id = "0";
            var result = await _movieService.GetMovies(int.Parse(id));

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
