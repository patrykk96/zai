using backend.Data.DbModels;
using backend.Data.Dto;
using backend.Data.Enums;
using backend.Data.Models;
using backend.Repository;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _repo;
        private readonly IRepository<Review> _reviewRepo;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<FavouriteMovie> _favouriteMovieRepo;
        private readonly IHostingEnvironment _hostingEnvironment;

        public MovieService(IRepository<Movie> repo, IRepository<Review> reviewRepo, UserManager<User> userManager, IHostingEnvironment hostingEnvironment, IRepository<FavouriteMovie> favouriteMovieRepo)
        {
            _repo = repo;
            _reviewRepo = reviewRepo;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
            _favouriteMovieRepo = favouriteMovieRepo;
            _userManager = userManager;
        }



        //metoda dodania filmu
        public async Task<ResultDto<BaseDto>> AddMovie(MovieModel movieModel)
        {
            var result = new ResultDto<BaseDto>()
            {
                Error = null
            };


            //sprawdzam czy film o podanej nazwie istnieje
            bool exists = await _repo.Exists(x => x.Name == movieModel.Name);

            if (exists)
            {
                result.Error = "Film o podanej nazwie zosta� ju� utworzony";
                return result;
            }

            //Jesli zostal dodany obrazek zapisuje go
            string path = null;

            if (movieModel.Logo != null)
            {
                path = SaveFile(movieModel.Logo, movieModel.Name);
            }

            //tworze film i zapisuje w bazie danych
            var movie = new Movie()
            {
                Name = movieModel.Name,
                Description = movieModel.Description,
                Logo = path
            };

            try
            {
                await _repo.Add(movie);
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        public async Task<ResultDto<BaseDto>> UpdateMovie(int id, MovieModel movieModel)
        {
            var result = new ResultDto<BaseDto>()
            {
                Error = null
            };

            //sprawdzam czy podany film istnieje
            var movie = await _repo.GetEntity(x => x.Id == id);

            if (movie == null)
            {
                result.Error = "Nie znaleziono podanego filmu";
                return result;
            }

            //Je�li nazwa jest nowa, sprawdzam czy inny film ju� jej nie ma
            if (movie.Name != movieModel.Name)
            {
                bool exists = await _repo.Exists(x => x.Name == movieModel.Name);

                if (exists)
                {
                    result.Error = "Podana nazwa filmu jest ju� zaj�ta";
                    return result;
                }
            }

            //Jesli zmieniono obrazek, zapisuje go
            string path = "";
            if (movieModel.Logo != null)
            {
                path = SaveFile(movieModel.Logo, movie.Name);
            }

            //edycja filmu
            if (movieModel.Name != "null")
            {
                movie.Name = movieModel.Name;
            }
            if (movieModel.Description != "null")
            {
                movie.Description = movieModel.Description;
            }

            if (path.Length > 0) movie.Logo = path;

            try
            {
                await _repo.Update(movie);
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        //usuwanie filmu
        public async Task<ResultDto<BaseDto>> DeleteMovie(int id)
        {
            var result = new ResultDto<BaseDto>()
            {
                Error = null
            };

            var movie = await _repo.GetEntity(x => x.Id == id);

            if (movie == null)
            {
                result.Error = "Nie odnaleziono podanego filmu";
                return result;
            }

            try
            {
                await _repo.Delete(movie);
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }

            return result;
        }

        //pobranie pojedynczego filmu
        public async Task<ResultDto<MovieDto>> GetMovie(int movieid, ClaimsPrincipal user)
        {
            var result = new ResultDto<MovieDto>()
            {
                Error = null
            };

            //probuje uzyskac wskazany film
            var movie = await _repo.GetEntity(x => x.Id == movieid);

            if (movie == null)
            {
                result.Error = "Nie odnaleziono filmu";
                return result;
            }

            string userId;
            // sprawdzam czy uzytkownik istnieje
            if (user.Identity.Name != null) {
                var userIdentity = await _userManager.FindByEmailAsync(user.Identity.Name);
                userId = userIdentity.Id;
            }
            else
            {
                userId = null;
            }
            int userRating; // zmienna dla oceny filmu aktualnego usera


            //probuje uzyskac recenzje aktualnego usera
            var userReview = await _reviewRepo.GetEntity(x => x.UserId == userId && x.MovieId == movieid);

            if (userReview == null)
            {
                userRating = 0;
            }
            else
            {
                userRating = userReview.Score;
            }


            //pobieram recenzje do obliczenia sredniej oceny filmu
            var reviews = await _reviewRepo.GetBy(x => x.MovieId == movieid);

            //zmienne do liczenia sredniej
            double reviewScoreSum = 0;
            double numberOfReviews = 0;

            //licze srednia ocene filmu z poszczegolnych recenzji
            foreach (var review in reviews)
            {
                reviewScoreSum += review.Score;
                numberOfReviews++;
            }

            bool isFavourite = false; 
            //sprawdzenie czy film jest ustawiony jako ulubiony dla zalogowanego u�ytkownika
            if (user != null)
            {
                var userIdentity = await _userManager.FindByEmailAsync(user.Identity.Name);
                var userId = userIdentity.Id;
                isFavourite = await _favouriteMovieRepo.Exists(x => x.MovieId == id && x.UserId == userId);
            }
            //tworze obiekt z filmem i zwracam go
            var movieToSend = new MovieDto()
            {
                Id = movie.Id,
                Description = movie.Description,
                Name = movie.Name,
                Logo = movie.Logo,
                UserRating = userRating,
                UsersAverage = reviewScoreSum/numberOfReviews,
                IsFavourite = isFavourite
            };

            result.SuccessResult = movieToSend;

            return result;
        }

        //zwr�cenie listy filmow
        public async Task<ResultDto<ListMovieDto>> GetMovies()
        {
            var result = new ResultDto<ListMovieDto>()
            {
                Error = null
            };


            string userId;
            // sprawdzam czy uzytkownik istnieje
            if (user.Identity.Name != null)
            {
                var userIdentity = await _userManager.FindByEmailAsync(user.Identity.Name);
                userId = userIdentity.Id;
            }
            else
            {
                userId = null;
            }

            var movies = await _repo.GetAll();

            List<MovieDto> moviesToSend = new List<MovieDto>();

            //tworze liste filmow do zwrocenia
            foreach (var movie in movies)
            {
                int userRating = 0; // zmienna dla oceny filmu aktualnego usera

                //pobieram recenzje do obliczenia sredniej oceny filmu
                var reviews = await _reviewRepo.GetBy(x => x.MovieId == movie.Id);

                //zmienne do liczenia sredniej
                double reviewScoreSum = 0;
                double numberOfReviews = 0;

                //licze srednia ocene filmu z poszczegolnych recenzji
                foreach (var review in reviews)
                {
                    if (review.UserId == userId)
                    {
                        userRating = review.Score;
                    }

                    reviewScoreSum +=review.Score;
                    numberOfReviews++;
                }

                var m = new MovieDto()
                {
                    Id = movie.Id,
                    Description = movie.Description,
                    Logo = movie.Logo,
                    Name = movie.Name,
                    UserRating = userRating,
                    UsersAverage = reviewScoreSum/numberOfReviews,
                };

                moviesToSend.Add(m);
            }

            var movieList = new ListMovieDto()
            {
                List = moviesToSend
            };

            result.SuccessResult = movieList;

            return result;
        }

        public async Task<ResultDto<BaseDto>> AddFavouriteMovie(ClaimsPrincipal user, int movieId)
        {
            var result = new ResultDto<BaseDto>()
            {
                Error = null
            };

            try
            {

                var userIdentity = await _userManager.FindByEmailAsync(user.Identity.Name);

                var userId = userIdentity.Id;

                if (string.IsNullOrEmpty(userId))
                {
                    result.Error = "Nie znaleziono u�ytkownika";

                    return result;
                }

                var movieExists = await _repo.Exists(x => x.Id == movieId);

                if (!movieExists)
                {
                    result.Error = "Nie znaleziono filmu";

                    return result;
                }

                var favouriteMovieAlreadyExists = await _favouriteMovieRepo.Exists(x => x.UserId == userId && x.MovieId == movieId);

                if (favouriteMovieAlreadyExists)
                {
                    result.Error = "Ten film zosta� ju� dodany jako ulubiony";

                    return result;
                }

                var favouriteMovie = new FavouriteMovie()
                {
                    MovieId = movieId,
                    UserId = userId
                };

                await _favouriteMovieRepo.Add(favouriteMovie);

                return result;

            }
            catch (Exception)
            {
                result.Error = "Wyst�pi� b��d";

                return result;
            }

        }

        public async Task<ResultDto<BaseDto>> DeleteFavouriteMovie(ClaimsPrincipal user, int movieId)
        {
            var result = new ResultDto<BaseDto>()
            {
                Error = null
            };

            try
            {
                var userIdentity = await _userManager.FindByEmailAsync(user.Identity.Name);

                var userId = userIdentity.Id;

                if (string.IsNullOrEmpty(userId))
                {
                    result.Error = "Nie znaleziono u�ytkownika";

                    return result;
                }

                var movieExists = await _repo.Exists(x => x.Id == movieId);

                if (!movieExists)
                {
                    result.Error = "Nie znaleziono filmu";

                    return result;
                }

                var favouriteMovie = await _favouriteMovieRepo.GetEntity(x => x.UserId == userId && x.MovieId == movieId);

                if (favouriteMovie == null)
                {
                    result.Error = "Ten film nie jest dodany jako ulubiony";

                    return result;
                }

                await _favouriteMovieRepo.Delete(favouriteMovie);

                return result;

            }
            catch (Exception)
            {
                result.Error = "Wyst�pi� b��d";

                return result;
            }

        }

        public async Task<ResultDto<ListMovieDto>> GetFavouriteMovies(ClaimsPrincipal user)
        {
            var result = new ResultDto<ListMovieDto>()
            {
                Error = null
            };

            try
            {
                var movies = await _repo.GetAll();

                var userIdentity = await _userManager.FindByEmailAsync(user.Identity.Name);

                var userId = userIdentity.Id;

                List<MovieDto> moviesToSend = new List<MovieDto>();

                foreach (var movie in movies)
                {
                    bool isFavourite = await _favouriteMovieRepo.Exists(x => x.UserId == userId && x.MovieId == movie.Id);

                    if (isFavourite)
                    {
                        var m = new MovieDto()
                        {
                            Id = movie.Id,
                            Description = movie.Description,
                            Logo = movie.Logo,
                            Name = movie.Name,
                        };

                        moviesToSend.Add(m);
                    } 
                }

                var movieList = new ListMovieDto()
                {
                    List = moviesToSend
                };

                result.SuccessResult = movieList;

                return result;
            }
            catch(Exception)
            {
                result.Error = "Wyst�pi� b��d";

                return result;
            }
               
        }

        //metoda zapisujaca obrazek na dysku
        private string SaveFile(IFormFile image, string name)
        {
            string path = "";

            var folderPath = _hostingEnvironment.WebRootPath + "\\uploads\\";
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                name = name.Replace(c.ToString(), "");
            }
            var fileName = name + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(folderPath, fileName);
            using (FileStream fs = File.Create(filePath))
            {
                image.CopyTo(fs);
                fs.Flush();
            }

            if (image.FileName.Length > 0)
            {
                path = @"https://localhost:44351/api/image/" + $"{fileName}";
            }
            else
            {
                path = @"https://localhost:44351/api/image/" + $"tommy-wiseau.jpg";
            }
            

            return path;
        }

    }
}