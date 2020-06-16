using backend.Data.DbModels;
using backend.Data.Models;
using backend.Repository;
using backend.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace backend.Tests
{
    public class CommentTests
    {
        [Fact]
        public async void ShouldNotAddCommentIfUserDoesNotExist()
        {
            
            var commentModel = new CommentModel()
            {
                Content = "RandomContentOfComment",
                ReviewId = 10,
            };

            string email = "test@mail.com";
            string username = "Test";
            string role = "user";
            string userId = Guid.NewGuid().ToString();

            var userDb = new User()
            {
                Id = "",
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var commentRepo = new Mock<IRepository<Comment>>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var commentService = new CommentService(repo.Object, reviewRepo.Object, userManager.Object, commentRepo.Object);


            var result = await commentService.AddComment(commentModel, user);

            string error = "Autor komentarza nie został odnaleziony";

            Assert.Contains(error, result.Error);
        }


        [Fact]
        public async void ShouldNotAddCommentIfReviewDoesNotExist()
        {

            var commentModel = new CommentModel()
            {
                Content = "RandomContentOfComment",
                ReviewId = 10,
            };

            string email = "test@mail.com";
            string username = "Test";
            string role = "user";
            string userId = Guid.NewGuid().ToString();

            var userDb = new User()
            {
                Id = userId,
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var commentRepo = new Mock<IRepository<Comment>>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);


            reviewRepo.Setup(x => x.Exists(It.IsAny<Expression<Func<Review, bool>>>())).Returns(Task.FromResult(false));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var commentService = new CommentService(repo.Object, reviewRepo.Object, userManager.Object, commentRepo.Object);


            var result = await commentService.AddComment(commentModel, user);

            string error = "Recenzja o takim id nie istnieje";

            Assert.Contains(error, result.Error);
        }


        [Fact]
        public async void ShouldNotDeleteCommentIfDoesNotExist()
        {

            int commentid = 5;

            Comment comment = null;

            string email = "test@mail.com";
            string username = "Test";
            string role = "user";
            string userId = Guid.NewGuid().ToString();

            var userDb = new User()
            {
                Id = userId,
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var commentRepo = new Mock<IRepository<Comment>>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);


            commentRepo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Comment, bool>>>())).Returns(Task.FromResult(comment));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var commentService = new CommentService(repo.Object, reviewRepo.Object, userManager.Object, commentRepo.Object);


            var result = await commentService.DeleteComment(commentid, user);

            string error = "Nie odnaleziono podanego komentarza";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldNotDeleteCommentIfUserIsNotAuthor()
        {

            int commentid = 5;

            var comment = new Comment()
            {
                AuthorId = "SomethingIncorrect",
                ReviewId = 5,
            };

            string email = "test@mail.com";
            string username = "Test";
            string role = "user";
            string userId = Guid.NewGuid().ToString();

            var userDb = new User()
            {
                Id = userId,
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var commentRepo = new Mock<IRepository<Comment>>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);


            commentRepo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Comment, bool>>>())).Returns(Task.FromResult(comment));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var commentService = new CommentService(repo.Object, reviewRepo.Object, userManager.Object, commentRepo.Object);


            var result = await commentService.DeleteComment(commentid, user);

            string error = "Tylko autor może usunąć swój komentarz";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldNotEditCommentIfUserHaveNoPermission()
        {
            var commentid = 5;

            var commentModel = new CommentModel()
            {
                Content = "'",
                ReviewId = 10,
            };

            var comment = new Comment()
            {
                AuthorId="SomethingOtherIncorrect",
                ReviewId= 5,
            };

            string email = "test@mail.com";
            string username = "Test";
            string role = "user";
            string userId = Guid.NewGuid().ToString();

            var userDb = new User()
            {
                Id = "SomethingIncorrect",
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var commentRepo = new Mock<IRepository<Comment>>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);


            commentRepo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Comment, bool>>>())).Returns(Task.FromResult(comment));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var commentService = new CommentService(repo.Object, reviewRepo.Object, userManager.Object, commentRepo.Object);


            var result = await commentService.UpdateComment(commentid, commentModel, user);

            string error = "Tylko autor może zmieniać swoje komentarze";

            Assert.Contains(error, result.Error);
        }


        [Fact]
        public async void ShouldNotEditCommentIfUserDoesNotExist()
        {
            var commentid = 5;

            var commentModel = new CommentModel()
            {
                Content = "'",
                ReviewId = 10,
            };

            Comment coment = null;

            string email = "test@mail.com";
            string username = "Test";
            string role = "user";
            string userId = Guid.NewGuid().ToString();

            var userDb = new User()
            {
                Id = "",
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var commentRepo = new Mock<IRepository<Comment>>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);


            commentRepo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Comment, bool>>>())).Returns(Task.FromResult(coment));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var commentService = new CommentService(repo.Object, reviewRepo.Object, userManager.Object, commentRepo.Object);


            var result = await commentService.UpdateComment(commentid, commentModel, user);

            string error = "Autor komentarza nie został odnaleziony";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldNotEditCommentIfNotExists()
        {
            var commentid = 5;

            var commentModel = new CommentModel()
            {
                Content = "'",
                ReviewId = 10,
            };

            Comment coment = null;

            string email = "test@mail.com";
            string username = "Test";
            string role = "user";
            string userId = Guid.NewGuid().ToString();

            var userDb = new User()
            {
                Id = "Test",
                Email = "test@mail.com"
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var commentRepo = new Mock<IRepository<Comment>>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);


            commentRepo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Comment, bool>>>())).Returns(Task.FromResult(coment));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var commentService = new CommentService(repo.Object, reviewRepo.Object, userManager.Object, commentRepo.Object);

            
            var result = await commentService.UpdateComment(commentid, commentModel, user);

            string error = "Komentarz nie został stworzony";

            Assert.Contains(error, result.Error);
        }


        public ClaimsPrincipal GetUserMock(string email, string username, string role)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Role, role),
                new Claim("Role", role)
            }, "mock"));

            return user;
        }

    }
}
