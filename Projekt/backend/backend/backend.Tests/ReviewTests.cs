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
    public class ReviewTests
    {

        [Fact]
        public async void ShouldNotAddReviewIfUserDoesNotExist()
        {

            var reviewModel = new ReviewModel()
            {
                Score = 5,
                Content = "Zawartość recenzji",
                MovieId = 10,
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

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var reviewService = new ReviewService(repo.Object, reviewRepo.Object, userManager.Object);


            var result = await reviewService.AddReview(reviewModel, user);

            string error = "Autor poradnika nie został odnaleziony";

            Assert.Contains(error, result.Error);
        }


        [Fact]
        public async void ShouldNotAddReviewIfMovieDoesNotExist()
        {

            var reviewModel = new ReviewModel()
            {
                Score = 5,
                Content = "Zawartość recenzji",
                MovieId = 10,
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

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(false));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var reviewService = new ReviewService(repo.Object, reviewRepo.Object, userManager.Object);


            var result = await reviewService.AddReview(reviewModel, user);

            string error = "Film o takim id nie istnieje";

            Assert.Contains(error, result.Error);
        }


        [Fact]
        public async void ShouldNotAddReviewIfReviewAlreadyExist()
        {

            var reviewModel = new ReviewModel()
            {
                Score = 5,
                Content = "Zawartość recenzji",
                MovieId = 10,
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

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(true));
            reviewRepo.Setup(x => x.Exists(It.IsAny<Expression<Func<Review, bool>>>())).Returns(Task.FromResult(true));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var reviewService = new ReviewService(repo.Object, reviewRepo.Object, userManager.Object);


            var result = await reviewService.AddReview(reviewModel, user);

            string error = "Recenzja została już stworzona";

            Assert.Contains(error, result.Error);
        }


        [Fact]
        public async void ShouldAddReviewIfDataIsCorrect()
        {

            var reviewModel = new ReviewModel()
            {
                Score = 5,
                Content = "Zawartość recenzji",
                MovieId = 10,
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

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(true));
            reviewRepo.Setup(x => x.Exists(It.IsAny<Expression<Func<Review, bool>>>())).Returns(Task.FromResult(false));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var reviewService = new ReviewService(repo.Object, reviewRepo.Object, userManager.Object);


            var result = await reviewService.AddReview(reviewModel, user);

            Assert.Null(result.Error);
        }


        [Fact]
        public async void ShouldNotUpdateReviewIfUserDoesNotExist()
        {

            int reviewid = 5;
            var reviewModel = new ReviewModel()
            {
                Score = 5,
                Content = "Zawartość recenzji",
                MovieId = 10,
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

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var reviewService = new ReviewService(repo.Object, reviewRepo.Object, userManager.Object);


            var result = await reviewService.UpdateReview(reviewid, reviewModel, user);

            string error = "Autor poradnika nie został odnaleziony";

            Assert.Contains(error, result.Error);
        }


        [Fact]
        public async void ShouldNotUpdateReviewIfMovieDoesNotExist()
        {

            int reviewid = 5;
            var reviewModel = new ReviewModel()
            {
                Score = 5,
                Content = "Zawartość recenzji",
                MovieId = 10,
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

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(false));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var reviewService = new ReviewService(repo.Object, reviewRepo.Object, userManager.Object);


            var result = await reviewService.UpdateReview(reviewid, reviewModel, user);

            string error = "Film o takim id nie istnieje";

            Assert.Contains(error, result.Error);
        }


        [Fact]
        public async void ShouldNotUpdateReviewIfReviewDoesNotExist()
        {

            int reviewid = 5;
            var reviewModel = new ReviewModel()
            {
                Score = 5,
                Content = "Zawartość recenzji",
                MovieId = 10,
            };

            Review review = null;

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

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(true));
            reviewRepo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Review, bool>>>())).Returns(Task.FromResult(review));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var reviewService = new ReviewService(repo.Object, reviewRepo.Object, userManager.Object);


            var result = await reviewService.UpdateReview(reviewid, reviewModel, user);

            string error = "Recenzja nie została stworzona";

            Assert.Contains(error, result.Error);
        }


        [Fact]
        public async void ShouldNotUpdateReviewIfUserIsNotAuthor()
        {

            int reviewid = 5;
            var reviewModel = new ReviewModel()
            {
                Score = 5,
                Content = "Zawartość recenzji",
                MovieId = 10,
            };

            var review = new Review()
            { 
                Score = 10,
                Content = "Recenzja z bazy danych",
                UserId = "Wartość niepoprawna",
            };

            string email = "test@mail.com";
            string username = "Test";
            string role = "user";
            string userId = Guid.NewGuid().ToString();

            var userDb = new User()
            {
                Id = "Inna wartość",
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(true));
            reviewRepo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Review, bool>>>())).Returns(Task.FromResult(review));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var reviewService = new ReviewService(repo.Object, reviewRepo.Object, userManager.Object);


            var result = await reviewService.UpdateReview(reviewid, reviewModel, user);

            string error = "Uzytkownik nie jest wlascicielem recenzji";

            Assert.Contains(error, result.Error);
        }


        [Fact]
        public async void ShouldUpdateReviewIfDataIsCorrect()
        {

            int reviewid = 5;
            var reviewModel = new ReviewModel()
            {
                Score = 5,
                Content = "Zawartość recenzji",
                MovieId = 10,
            };

            string email = "test@mail.com";
            string username = "Test";
            string role = "user";
            string userId = Guid.NewGuid().ToString();

            var review = new Review()
            {
                Score = 10,
                Content = "Recenzja z bazy danych",
                UserId = userId,
            };

            var userDb = new User()
            {
                Id = userId,
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(true));
            reviewRepo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Review, bool>>>())).Returns(Task.FromResult(review));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var reviewService = new ReviewService(repo.Object, reviewRepo.Object, userManager.Object);


            var result = await reviewService.UpdateReview(reviewid, reviewModel, user);

            Assert.Null(result.Error);
        }

        [Fact]
        public async void ShouldNotDeleteReviewIfReviewDoesNotExist()
        {

            int reviewid = 5;

            Review review = null;

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

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            reviewRepo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Review, bool>>>())).Returns(Task.FromResult(review));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var reviewService = new ReviewService(repo.Object, reviewRepo.Object, userManager.Object);


            var result = await reviewService.DeleteReview(reviewid, user);

            string error = "Nie odnaleziono podanej recenzji";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldNotDeleteReviewIfUserIsNotAuthor()
        {

            int reviewid = 5;

            var review = new Review()
            {
                Score = 10,
                Content = "Recenzja z bazy danych",
                UserId = "Wartość niepoprawna",
            };

            string email = "test@mail.com";
            string username = "Test";
            string role = "user";
            string userId = Guid.NewGuid().ToString();

            var userDb = new User()
            {
                Id = "Zła wartość",
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            reviewRepo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Review, bool>>>())).Returns(Task.FromResult(review));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var reviewService = new ReviewService(repo.Object, reviewRepo.Object, userManager.Object);


            var result = await reviewService.DeleteReview(reviewid, user);

            string error = "Tylko autor może usunąć swoją recenzje";

            Assert.Contains(error, result.Error);
        }


        [Fact]
        public async void ShouldDeleteReviewIfDataIsCorrect()
        {

            int reviewid = 5;

            string email = "test@mail.com";
            string username = "Test";
            string role = "user";
            string userId = Guid.NewGuid().ToString();

            var review = new Review()
            {
                Score = 10,
                Content = "Recenzja z bazy danych",
                UserId = userId,
            };

            var userDb = new User()
            {
                Id = userId,
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            reviewRepo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Review, bool>>>())).Returns(Task.FromResult(review));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var reviewService = new ReviewService(repo.Object, reviewRepo.Object, userManager.Object);


            var result = await reviewService.DeleteReview(reviewid, user);

            Assert.Null(result.Error);
        }


        [Fact]
        public async void ShouldNotGetReviewIfReviewDoesNotExist()
        {

            int reviewid = 5;

            Review review = null;

            string email = "test@mail.com";
            string username = "Test";
            string role = "user";
            string userId = Guid.NewGuid().ToString();

            var userDb = new User()
            {
                Id = "Zła wartość",
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            reviewRepo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Review, bool>>>())).Returns(Task.FromResult(review));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var reviewService = new ReviewService(repo.Object, reviewRepo.Object, userManager.Object);


            var result = await reviewService.GetReview(reviewid, user);

            string error = "Nie odnaleziono recenzji";

            Assert.Contains(error, result.Error);
        }


        [Fact]
        public async void ShouldNotGetReviewIfMovieDoesNotExist()
        {

            int reviewid = 5;

            var review = new Review()
            {
                Score = 10,
                Content = "Recenzja z bazy danych",
                UserId = "Wartość niepoprawna",
            };

            Movie movie = null;

            string email = "test@mail.com";
            string username = "Test";
            string role = "user";
            string userId = Guid.NewGuid().ToString();

            var userDb = new User()
            {
                Id = "Zła wartość",
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            repo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(movie));
            reviewRepo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Review, bool>>>())).Returns(Task.FromResult(review));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var reviewService = new ReviewService(repo.Object, reviewRepo.Object, userManager.Object);


            var result = await reviewService.GetReview(reviewid, user);

            string error = "Nie odnaleziono filmu";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldGetReviewIfDataIsCorrect()
        {

            int reviewid = 5;

            var review = new Review()
            {
                Score = 10,
                Content = "Recenzja z bazy danych",
                UserId = "Inna Wartość",               
            };

            var movie = new Movie()
            {
                Id = 10,
                Name = "Szybcy i Wściekli",
            };

            string email = "test@mail.com";
            string username = "Test";
            string role = "user";
            string userId = Guid.NewGuid().ToString();

            var userDb = new User()
            {
                Id = "Wartość",
                Email = "",
                UserName = "Nazwa Użytkownika",
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            repo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(movie));
            reviewRepo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Review, bool>>>())).Returns(Task.FromResult(review));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));
            userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var reviewService = new ReviewService(repo.Object, reviewRepo.Object, userManager.Object);


            var result = await reviewService.GetReview(reviewid, user);

            Assert.Null(result.Error);
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
