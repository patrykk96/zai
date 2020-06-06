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
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : Controller
    {

        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        [HttpPost("addComment")]
        public async Task<IActionResult> AddComment(CommentModel commentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var result = await _commentService.AddComment(commentModel, user);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpDelete("deleteComment/{commentid}")]
        public async Task<IActionResult> DeleteComment(int commentid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var result = await _commentService.DeleteComment(commentid, user);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpPatch("updateComment/{commentid}")]
        public async Task<IActionResult> UpdateComment(int commentid, CommentModel commentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var result = await _commentService.UpdateComment(commentid, commentModel, user);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [AllowAnonymous]
        [HttpGet("getComment/{commentid}")]
        public async Task<IActionResult> GetComment(int commentid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var result = await _commentService.GetComment(commentid, user);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [AllowAnonymous]
        [HttpGet("getComments/{reviewid}")]
        public async Task<IActionResult> GetComments(int reviewid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = User;

            var result = await _commentService.GetComments(reviewid, user);

            if (result.Error != null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
