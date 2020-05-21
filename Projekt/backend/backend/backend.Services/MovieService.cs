using backend.Data.DbModels;
using backend.Data.Dto;
using backend.Data.Enums;
using backend.Data.Models;
using backend.Repository;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _repo;
        private readonly IRepository<Review> _reviewRepo;
        private readonly IHostingEnvironment _hostingEnvironment;

        public MovieService(IRepository<Movie> repo, IRepository<Review> reviewRepo, IHostingEnvironment hostingEnvironment)
        {
            _repo = repo;
            _reviewRepo = reviewRepo;
            _hostingEnvironment = hostingEnvironment;
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
                result.Error = "Film o podanej nazwie zosta³ ju¿ utworzony";
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

            //Jeœli nazwa jest nowa, sprawdzam czy inny film ju¿ jej nie ma
            if (movie.Name != movieModel.Name)
            {
                bool exists = await _repo.Exists(x => x.Name == movieModel.Name);

                if (exists)
                {
                    result.Error = "Podana nazwa filmu jest ju¿ zajêta";
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
        public async Task<ResultDto<MovieDto>> GetMovie(int id)
        {
            var result = new ResultDto<MovieDto>()
            {
                Error = null
            };

            //probuje uzyskac wskazany film
            var movie = await _repo.GetEntity(x => x.Id == id);

            if (movie == null)
            {
                result.Error = "Nie odnaleziono filmu";
                return result;
            }

            //tworze obiekt z filmem i zwracam go
            var movieToSend = new MovieDto()
            {
                Id = movie.Id,
                Description = movie.Description,
                Name = movie.Name,
                Logo = movie.Logo,
                //Rating = movie.Rating,
            };

            result.SuccessResult = movieToSend;

            return result;
        }

        //zwrócenie listy filmow
        public async Task<ResultDto<ListMovieDto>> GetMovies()
        {
            var result = new ResultDto<ListMovieDto>()
            {
                Error = null
            };

            var movies = await _repo.GetAll();

            List<MovieDto> moviesToSend = new List<MovieDto>();

            //tworze liste filmow do zwrocenia
            foreach (var movie in movies)
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

            var movieList = new ListMovieDto()
            {
                List = moviesToSend
            };

            result.SuccessResult = movieList;

            return result;
        }

        //dodaj recenzje
        public async Task<ResultDto<BaseDto>> AddReview(ReviewModel reviewModel)
        {

            var result = new ResultDto<BaseDto>()
            {
                Error = null
            };

            //sprawdzam czy film istnieje
            var movieExists = await _repo.Exists(x => x.Id == reviewModel.MovieId);

            if (!movieExists)
            {
                result.Error = "Nie odnaleziono filmu";
                return result;
            }

            //sprawdzam czy ten u¿ytkownik ju¿ doda³ recenzje do wybranego filmu
            var oldReview = await _reviewRepo.GetEntity(x => x.MovieId == reviewModel.MovieId
                                                        && x.UserId == reviewModel.UserId);


            //jeœli recenzja ju¿ jest, zmieniam jej wartoœæ na now¹, w przeciwnym wypadku tworze nowa recenzje
            if (oldReview != null)
            {
                if (oldReview.Score != reviewModel.Score
                    && oldReview.Content != reviewModel.Content)
                {
                    oldReview.Score = reviewModel.Score;
                    oldReview.Content = reviewModel.Content;
                    oldReview.Updated = DateTime.Now;
                    await _reviewRepo.Update(oldReview);
                }
            }
            else
            {
                var review = new Review()
                {
                    UserId = reviewModel.UserId,
                    MovieId = reviewModel.MovieId,
                    Score = reviewModel.Score,
                    Content = reviewModel.Content,
                    Created = DateTime.Now
                };
                await _reviewRepo.Add(review);
            }
                       
            return result;

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