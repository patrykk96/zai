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
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("addReview")]
        public async Task<IActionResult> AddReview(ReviewModel reviewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var result = await _reviewService.AddReview(reviewModel, user);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPatch("updateReview/{reviewid}")]
        public async Task<IActionResult> UpdateReview(int reviewid, ReviewModel reviewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var result = await _reviewService.UpdateReview(reviewid, reviewModel, user);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpDelete("deleteReview/{reviewid}")]
        public async Task<IActionResult> DeleteReview(int reviewid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var result = await _reviewService.DeleteReview(reviewid, user);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [AllowAnonymous]
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
