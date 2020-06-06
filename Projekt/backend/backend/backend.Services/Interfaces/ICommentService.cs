using backend.Data.Dto;
using backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services.Interfaces
{
    public interface ICommentService
    {
        Task<ResultDto<BaseDto>> AddComment(CommentModel commentModel, ClaimsPrincipal user);
        Task<ResultDto<BaseDto>> UpdateComment(int commentid, CommentModel commentModel, ClaimsPrincipal user);
        Task<ResultDto<BaseDto>> DeleteComment(int commentid, ClaimsPrincipal user);
        Task<ResultDto<CommentDto>> GetComment(int commentid, ClaimsPrincipal user);
        Task<ResultDto<ListCommentDto>> GetComments(int reviewid, ClaimsPrincipal user);
    }
}
