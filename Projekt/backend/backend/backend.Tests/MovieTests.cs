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
    public class MovieTests
    {
        [Fact]
        public async void ShouldNotAddMovieIfExistsWithSameName()
        {
            var name = "film";
            var description = "opis filmu";
            var logo = new ImageModel();

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(true));

            var movieModel = new MovieModel()
            {
                Name = name,
                Description = description,
                Logo = logo.Image
            };

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.AddMovie(movieModel);

            string error = "Film o podanej nazwie został już utworzony";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldAddMovieIfDataIsCorrect()
        {
            var name = "film";
            var description = "opis filmu";
            var logo = new ImageModel();

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(false));

            var movieModel = new MovieModel()
            {
                Name = name,
                Description = description,
                Logo = logo.Image
            };

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.AddMovie(movieModel);

            Assert.Null(result.Error);
        }

        [Fact]
        public async void ShouldNotUpdateMovieIfMovieNotFound()
        {
            var movieId = 1;
            var name = "film";
            var description = "opis filmu";
            var logo = new ImageModel();

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            Movie movieDb = null;

            repo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(movieDb));

            var movieModel = new MovieModel()
            {
                Name = name,
                Description = description,
                Logo = logo.Image
            };

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.UpdateMovie(movieId, movieModel);

            string error = "Nie znaleziono podanego filmu";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldNotUpdateMovieAnotherMovieWithSameNameFound()
        {
            var movieId = 1;
            var name = "film";
            var description = "opis filmu";
            var logo = new ImageModel();

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            Movie movieDb = new Movie();

            repo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(movieDb));
            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(true));

            var movieModel = new MovieModel()
            {
                Name = name,
                Description = description,
                Logo = logo.Image
            };

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.UpdateMovie(movieId, movieModel);

            string error = "Podana nazwa filmu jest już zajęta";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldUpdateMovieIfDataIsCorrect()
        {
            var movieId = 1;
            var name = "film";
            var description = "opis filmu";
            var logo = new ImageModel();

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            Movie movieDb = new Movie();

            repo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(movieDb));
            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(false)); 

            var movieModel = new MovieModel()
            {
                Name = name,
                Description = description,
                Logo = logo.Image
            };

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.UpdateMovie(movieId, movieModel);

            Assert.Null(result.Error);
        }

        [Fact]
        public async void ShouldNotDeleteMovieIfMovieNotFound()
        {
            var movieId = 1;

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            Movie movieDb = null;

            repo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(movieDb));

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.DeleteMovie(movieId);

            string error = "Nie odnaleziono podanego filmu";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldDeleteMovieIfMovieExists()
        {
            var movieId = 1;

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            Movie movieDb = new Movie();

            repo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(movieDb));

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.DeleteMovie(movieId);

            Assert.Null(result.Error);
        }


        [Fact]
        public async void ShouldNotGetMovieIfMovieNotFound()
        {

            var movieId = 1;
            string email = "test@mail.com";
            string username = "Test";
            string role = "user";

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            Movie movieDb = null;

            repo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(movieDb));

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.GetMovie(movieId, user);

            var error = "Nie odnaleziono filmu";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldGetMovieIfMovieFound()
        {

            var movieId = 1;
            string email = "test@mail.com";
            string username = "Test";
            string role = "user";

            var userDb = new User()
            {
                Id = "",
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            Movie movieDb = new Movie();

            List<Review> reviews = new List<Review>();

            repo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(movieDb));
            reviewRepo.Setup(x => x.GetBy(It.IsAny<Expression<Func<Review, bool>>>())).Returns(Task.FromResult(reviews));
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.GetMovie(movieId, user);

            Assert.Null(result.Error);
        }

        [Fact]
        public async void ShouldNotAddMovieToFavouritesIfUserNotFound()
        {
            var movieId = 1;
            string email = "test@mail.com";
            string username = "Test";
            string role = "user";

            var userDb = new User()
            {
                Id = "",
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            List<Review> reviews = new List<Review>();

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.AddFavouriteMovie(user, movieId);

            var error = "Nie znaleziono użytkownika";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldNotAddMovieToFavouritesIfMovieNotFound()
        {
            var movieId = 1;
            string email = "test@mail.com";
            string username = "Test";
            string role = "user";

            var userDb = new User()
            {
                Id = "id",
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            List<Review> reviews = new List<Review>();

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));
            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(false));

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.AddFavouriteMovie(user, movieId);

            var error = "Nie znaleziono filmu";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldNotAddMovieToFavouritesIfAlreadyFavourite()
        {
            var movieId = 1;
            string email = "test@mail.com";
            string username = "Test";
            string role = "user";

            var userDb = new User()
            {
                Id = "id",
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            List<Review> reviews = new List<Review>();

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));
            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(true));
            favouriteMovieRepo.Setup(x => x.Exists(It.IsAny<Expression<Func<FavouriteMovie, bool>>>())).Returns(Task.FromResult(true));

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.AddFavouriteMovie(user, movieId);

            var error = "Ten film został już dodany jako ulubiony";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldAddMovieToFavouritesIfTheresNoErrors()
        {
            var movieId = 1;
            string email = "test@mail.com";
            string username = "Test";
            string role = "user";

            var userDb = new User()
            {
                Id = "id",
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            List<Review> reviews = new List<Review>();

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));
            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(true));
            favouriteMovieRepo.Setup(x => x.Exists(It.IsAny<Expression<Func<FavouriteMovie, bool>>>())).Returns(Task.FromResult(false));

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.AddFavouriteMovie(user, movieId);

            Assert.Null(result.Error);
        }

        [Fact]
        public async void ShouldNotDeleteMovieFromFavouritesIfUserNotFound()
        {
            var movieId = 1;
            string email = "test@mail.com";
            string username = "Test";
            string role = "user";

            var userDb = new User()
            {
                Id = "",
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            List<Review> reviews = new List<Review>();

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.DeleteFavouriteMovie(user, movieId);

            var error = "Nie znaleziono użytkownika";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldNotDeleteMovieFromFavouritesIfMovieNotFound()
        {
            var movieId = 1;
            string email = "test@mail.com";
            string username = "Test";
            string role = "user";

            var userDb = new User()
            {
                Id = "id",
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            List<Review> reviews = new List<Review>();

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));
            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(false));

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.DeleteFavouriteMovie(user, movieId);

            var error = "Nie znaleziono filmu";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldNotDeleteMovieFromFavouritesIfNotFavourite()
        {
            var movieId = 1;
            string email = "test@mail.com";
            string username = "Test";
            string role = "user";

            var userDb = new User()
            {
                Id = "id",
                Email = ""
            };

            FavouriteMovie favouriteMovie = null;

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            List<Review> reviews = new List<Review>();

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));
            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(true));
            favouriteMovieRepo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<FavouriteMovie, bool>>>())).Returns(Task.FromResult(favouriteMovie));

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.DeleteFavouriteMovie(user, movieId);

            var error = "Ten film nie jest dodany jako ulubiony";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldDeleteMovieFromFavouritesIfTheresNoErrors()
        {
            var movieId = 1;
            string email = "test@mail.com";
            string username = "Test";
            string role = "user";
            string userId = Guid.NewGuid().ToString();

            var userDb = new User()
            {
                Id = "id",
                Email = ""
            };

            var favouriteMovie = new FavouriteMovie()
            {
                MovieId = movieId,
                UserId = "id"
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            List<Review> reviews = new List<Review>();

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));
            repo.Setup(x => x.Exists(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(Task.FromResult(true));
            favouriteMovieRepo.Setup(x => x.GetEntity(It.IsAny<Expression<Func<FavouriteMovie, bool>>>())).Returns(Task.FromResult(favouriteMovie));

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.DeleteFavouriteMovie(user, movieId);

            Assert.Null(result.Error);
        }

        [Fact]
        public async void ShouldNotGetFavouriteMoviesIfUserNotFound()
        {
            string email = "test@mail.com";
            string username = "Test";
            string role = "user";

            var userDb = new User()
            {
                Id = "",
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            List<Review> reviews = new List<Review>();

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.GetFavouriteMovies(user);

            var error = "Nie znaleziono użytkownika";

            Assert.Contains(error, result.Error);
        }

        [Fact]
        public async void ShouldGetFavouriteMoviesIfUserFound()
        {
            string email = "test@mail.com";
            string username = "Test";
            string role = "user";

            var userDb = new User()
            {
                Id = "id",
                Email = ""
            };

            var user = GetUserMock(email, username, role);

            var movies = new List<Movie>();

            var repo = new Mock<IRepository<Movie>>();
            var reviewRepo = new Mock<IRepository<Review>>();
            var favouriteMovieRepo = new Mock<IRepository<FavouriteMovie>>();
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            List<Review> reviews = new List<Review>();

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(userDb));
            repo.Setup(x => x.GetAll()).Returns(Task.FromResult(movies));

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object, favouriteMovieRepo.Object);

            var result = await movieService.GetFavouriteMovies(user);

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
