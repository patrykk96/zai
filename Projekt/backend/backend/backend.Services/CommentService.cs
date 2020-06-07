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
    public class CommentService : ICommentService
    {
        private readonly IRepository<Movie> _repo;
        private readonly IRepository<Review> _reviewRepo;
        private readonly IRepository<Comment> _commentRepo;
        private readonly UserManager<User> _userManager;

        public CommentService(IRepository<Movie> repo, IRepository<Review> reviewRepo, UserManager<User> userManager,
            IRepository<Comment> commentRepo)
        {
            _repo = repo;
            _reviewRepo = reviewRepo;
            _commentRepo = commentRepo;
            _userManager = userManager;
        }


        public async Task<ResultDto<BaseDto>> AddComment(CommentModel commentModel, ClaimsPrincipal user)
        {
            var result = new ResultDto<BaseDto>()
            {
                Error = null
            };

            //sprawdzam czy uzytkownik istnieje
            string userId = await _userManager.GetId(user);

            if (string.IsNullOrEmpty(userId))
            {
                result.Error = "Autor komentarza nie został odnaleziony";
                return result;
            }

            //sprawdzam czy recenzja istnieje
            bool exists = await _reviewRepo.Exists(x => x.Id == commentModel.ReviewId);

            if (!exists)
            {
                result.Error = "Recenzja o takim id nie istnieje";
                return result;
            }

            //tworze komentarz i zapisuje w bazie danych
            var comment = new Comment()
            {
                Content = commentModel.Content,
                Created = DateTime.Now,
                AuthorId = userId,
                ReviewId = commentModel.ReviewId,
            };

            try
            {
                await _commentRepo.Add(comment);
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }
    

        public async Task<ResultDto<BaseDto>> DeleteComment(int commentid, ClaimsPrincipal user)
        {
            var result = new ResultDto<BaseDto>()
            {
                Error = null
            };

            //sprawdzam czy uzytkownik istnieje
            string userId = await _userManager.GetId(user);

            var comment = await _commentRepo.GetEntity(x => x.Id == commentid);

            if (comment == null)
            {
                result.Error = "Nie odnaleziono podanego komentarza";
                return result;
            }

            if (comment.AuthorId != userId)
            {
                result.Error = "Tylko autor może usunąć swój komentarz";
                return result;
            }

            try
            {
                await _commentRepo.Delete(comment);
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<ResultDto<BaseDto>> UpdateComment(int commentid, CommentModel commentModel, ClaimsPrincipal user)
        {
            var result = new ResultDto<BaseDto>()
            {
                Error = null
            };

            //sprawdzam czy uzytkownik istnieje
            string userId = await _userManager.GetId(user);

            if (string.IsNullOrEmpty(userId))
            {
                result.Error = "Autor komentarza nie został odnaleziony";
                return result;
            }

            //sprawdzam czy komentarz danej recenzji istnieje
            var comment = await _commentRepo.GetEntity(x => x.Id == commentid);

            if (comment == null)
            {
                result.Error = "Komentarz nie został stworzony";
                return result;
            }

            //zmiana komentarza pod warunkiem, ze to autor go zmienia
            if (comment.AuthorId == userId)
            {
                if (commentModel.Content != "null")
                {
                    comment.Content = commentModel.Content;
                }

                comment.Updated = DateTime.Now;
            }
            else
            {
                result.Error = "Tylko autor może zmieniać swoje komentarze";
                return result;
            }


            try
            {
                await _commentRepo.Update(comment);
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<ResultDto<CommentDto>> GetComment(int commentid, ClaimsPrincipal user)
        {
            var result = new ResultDto<CommentDto>()
            {
                Error = null
            };

            //probuje uzyskac wskazany komentarz
            var comment = await _commentRepo.GetEntity(x => x.Id == commentid);

            if (comment == null)
            {
                result.Error = "Nie odnaleziono komentarza";
                return result;
            }

            //pobieram nazwe uzytkownika
            var commentOwner = await _userManager.FindByIdAsync(comment.AuthorId);
            var commentOwnerName = commentOwner.UserName;

            //tworze obiekt z komentarzem i zwracam go
            var commentToSend = new CommentDto()
            {
                CommentId = comment.Id,
                Author = commentOwnerName,
                ReviewId = comment.ReviewId,
                Content = comment.Content,
            };

            result.SuccessResult = commentToSend;

            return result;
        }

        public async Task<ResultDto<ListCommentDto>> GetComments(int reviewid, ClaimsPrincipal user)
        {
            var result = new ResultDto<ListCommentDto>()
            {
                Error = null
            };

            var comments = await _commentRepo.GetBy(x => x.ReviewId == reviewid);

            List<CommentDto> commentsToSend = new List<CommentDto>();

            //tworze liste komentarzy do zwrocenia
            foreach (var comment in comments)
            {
                //pobieram nazwe uzytkownika
                var commentOwner = await _userManager.FindByIdAsync(comment.AuthorId);
                var commentOwnerName = commentOwner.UserName;

                var m = new CommentDto()
                {
                    CommentId = comment.Id,
                    Author = commentOwnerName,
                    ReviewId = comment.ReviewId,
                    Content = comment.Content,
                };

                commentsToSend.Add(m);
            }

            var commentsList = new ListCommentDto()
            {
                List = commentsToSend
            };

            result.SuccessResult = commentsList;

            return result;
        }
    }
}
