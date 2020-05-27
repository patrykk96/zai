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
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        [HttpPost("addReview/{content}/{rating}/{movieid}")]
        public async Task<IActionResult> AddReview(string content, int rating, int movieid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var reviewModel = new ReviewModel()
            {
                Score = rating,
                Content = content,
                MovieId = movieid
            };

            var result = await _reviewService.AddReview(reviewModel, user);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPatch("updateReview/{content}/{rating}/{movieid}")]
        public async Task<IActionResult> UpdateReview(string content, int rating, int movieid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var reviewModel = new ReviewModel()
            {
                Score = rating,
                Content = content,
                MovieId = movieid
            };

            var result = await _reviewService.UpdateReview(reviewModel, user);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpDelete("deleteReview/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var result = await _reviewService.DeleteReview(id, user);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpGet("getReview/{reviewid}")]
        public async Task<IActionResult> GetReview(int reviewid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var result = await _reviewService.GetReview(reviewid, user);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("getReviews/{movieid}")]
        public async Task<IActionResult> GetReviews(int movieid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var result = await _reviewService.GetReviews(movieid, user);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
