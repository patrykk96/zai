using backend.Data.DbModels;
using backend.Data.Models;
using backend.Repository;
using backend.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
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

            var movieService = new MovieService(repo.Object, reviewRepo.Object, userManager.Object, hostingEnvironment.Object);

            var result = await movieService.AddMovie(movieModel);

            string error = "Film o podanej nazwie został już utworzony";

            Assert.Contains(error, result.Error);
        }
    }
}
