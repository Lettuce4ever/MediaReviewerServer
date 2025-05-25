using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediaReviewerServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using MediaReviewerServer.DTO;
using Microsoft.AspNetCore.Routing.Constraints;
namespace MediaReviewerServer.Controllers
{
    [Route("api")]
    [ApiController]
    public class MediaReviewerAPIController : ControllerBase
    {
        //a variable to hold a reference to the db context!
        private MediaReviewerDbContext context;
        //a variable that hold a reference to web hosting interface (that provide information like the folder on which the server runs etc...)
        private IWebHostEnvironment webHostEnvironment;
        //Use dependency injection to get the db context and web host into the constructor
        public MediaReviewerAPIController(MediaReviewerDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.webHostEnvironment = env;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] DTO.LoginDTO loginDto)
        {
            try
            {
                HttpContext.Session.Clear(); //Logout any previous login attempt

                //Get model user class from DB with matching email. 
                Models.User? modelsUser = context.GetUser(loginDto.Email);

                //Check if user exist for this email and if password match, if not return Access Denied (Error 403) 
                if (modelsUser == null || modelsUser.Password != loginDto.Password)
                {
                    return Unauthorized();
                }

                //Login suceed! now mark login in session memory!
                HttpContext.Session.SetString("loggedInUser", modelsUser.Email);
                DTO.UserDTO dtoUser = new DTO.UserDTO(modelsUser);
                dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.UserID);
                return Ok(dtoUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("signup")]
        public IActionResult Signup([FromBody] DTO.UserDTO userDto)
        {
            try
            {
                HttpContext.Session.Clear(); //Logout any previous login attempt

                //Create model user class
                Models.User modelsUser = userDto.GetModels();

                context.Users.Add(modelsUser);
                context.SaveChanges();

                //User was added!
                DTO.UserDTO dtoUser = new DTO.UserDTO(modelsUser);
                return Ok(dtoUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("addgenre")]
        public IActionResult AddGenre([FromBody] DTO.GenreDTO genreDto)
        {
            try
            {
                string? userEmail = HttpContext.Session.GetString("loggedInUser");
                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized("User is not logged in");
                }

                //Create model genre class
                Models.Genre modelsGenre = genreDto.GetModels();

                context.Genres.Add(modelsGenre);
                context.SaveChanges();

                //User was added!
                DTO.GenreDTO dtoGenre = new DTO.GenreDTO(modelsGenre);
                return Ok(dtoGenre);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("addmovie")]
        public IActionResult AddMovie([FromBody] DTO.MovieDTO movieDto)
        {
            try
            {
                string? userEmail = HttpContext.Session.GetString("loggedInUser");
                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized("User is not logged in");
                }

                //Create model movie class
                Models.Movie modelsMovie = movieDto.GetModels();

                context.Movies.Add(modelsMovie);
                context.SaveChanges();

                
                DTO.MovieDTO dtoMovie = new DTO.MovieDTO(modelsMovie);
                return Ok(dtoMovie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //Get api/getgenres
        //This method is used to get all genres from the database and return a list of DTO.Genres
        [HttpGet("getgenres")]
        public IActionResult GetGenres()
        {
            try
            {
                List<DTO.GenreDTO> dtoGenres = new List<DTO.GenreDTO>();
                List<Genre> modelgenres = context.Genres.ToList();
                foreach (Genre var in modelgenres)
                {
                    dtoGenres.Add(new DTO.GenreDTO()
                    {
                        GenreId = var.GenreId,
                        GenreName = var.GenreName
                    });
                }
                return Ok(dtoGenres);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Get api/getmovies
        //This method is used to get all movies from the database and return a list of DTO.Movies
        [HttpGet("getmovies")]
        public IActionResult GetMovies()
        {
            try
            {
                List<DTO.MovieDTO> dtoMovies = new List<DTO.MovieDTO>();
                List<Movie> modelmovies = context.Movies.Include(m => m.Genres).Include(m => m.Reviews).ToList();
                foreach (Movie var in modelmovies)
                {
                    DTO.MovieDTO movieDTO = new DTO.MovieDTO()
                    {
                        MovieId = var.MovieId,
                        MovieName = var.MovieName,
                        ReleaseYear = var.ReleaseYear,
                        Length = var.Length,
                        Description = var.Description,
                        Rating = var.Rating,
                        Image = var.Image,
                        Trailer = var.Trailer,
                        Director = var.Director,
                        Star = var.Star,
                        Writer = var.Writer,
                        MultiDirectors = var.MultiDirectors,
                        MultiStars = var.MultiStars,
                        MultiWriters = var.MultiWriters,
                        Genres = new List<DTO.GenreDTO>(),
                        Reviews = new List<DTO.ReviewDTO>()
                    };
                    foreach (Genre genre in var.Genres)
                    {
                        movieDTO.Genres.Add(new DTO.GenreDTO()
                        {
                            GenreId = genre.GenreId,
                            GenreName = genre.GenreName
                        });
                    }
                    foreach(Review review in var.Reviews)
                    {
                        movieDTO.Reviews.Add(new DTO.ReviewDTO()
                        {
                            ReviewId = review.ReviewId,
                            UserId = review.UserId,
                            MovieId = review.MovieId,
                            Rating = review.Rating,
                            Description = review.Description,
                            ReviewDate = review.ReviewDate
                        });
                    }

                    dtoMovies.Add(movieDTO);

                }
                return Ok(dtoMovies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addreview")]
        public IActionResult AddReview([FromBody] DTO.ReviewDTO reviewDto)
        {
            try
            {
                string? userEmail = HttpContext.Session.GetString("loggedInUser");
                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized("User is not logged in");
                }
                //Create model review class
                Models.Review modelsReview = reviewDto.GetModels();

                context.Reviews.Add(modelsReview);
                context.SaveChanges();

                //Review was added!
                DTO.ReviewDTO dtoReview = new DTO.ReviewDTO(modelsReview);


                double rating=0;
                int count = 0;
                List<DTO.ReviewDTO> newdtoReviews = new List<DTO.ReviewDTO>();
                List<Review> modelreviews = context.GetReviewsByMovie((int)(reviewDto.MovieId));
                foreach (Review var in modelreviews)
                {
                    newdtoReviews.Add(new DTO.ReviewDTO()
                    {
                        ReviewId = var.ReviewId,
                        UserId = var.UserId,
                        MovieId = var.MovieId,
                        Rating = var.Rating,
                        Description = var.Description,
                        ReviewDate = var.ReviewDate
                    });
                }
                foreach (DTO.ReviewDTO var in newdtoReviews)
                {
                    rating += var.Rating;
                    count++;
                }
                rating = rating / count;
                context.SetMovieRating((int)(reviewDto.MovieId), rating);



                return Ok(dtoReview);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("editreview")]
        public IActionResult EditReview([FromBody] DTO.ReviewDTO reviewDto)
        {
            try
            {
                string? userEmail = HttpContext.Session.GetString("loggedInUser");
                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized("User is not logged in");
                }
                //Create model review class
                Models.Review modelsReview = reviewDto.GetModels();

                context.SetReviewRating((int)reviewDto.MovieId, (int)reviewDto.UserId, modelsReview.Rating);
                context.SetReviewDescription((int)reviewDto.MovieId, (int)reviewDto.UserId, modelsReview.Description);
                context.SetReviewDate((int)reviewDto.MovieId, (int)reviewDto.UserId, modelsReview.ReviewDate);
                context.SaveChanges();

                //Review was edited!
                DTO.ReviewDTO dtoReview = new DTO.ReviewDTO(modelsReview);


                double rating = 0;
                int count = 0;
                List<DTO.ReviewDTO> newdtoReviews = new List<DTO.ReviewDTO>();
                List<Review> modelreviews = context.GetReviewsByMovie((int)(reviewDto.MovieId));
                foreach (Review var in modelreviews)
                {
                    newdtoReviews.Add(new DTO.ReviewDTO()
                    {
                        ReviewId = var.ReviewId,
                        UserId = var.UserId,
                        MovieId = var.MovieId,
                        Rating = var.Rating,
                        Description = var.Description,
                        ReviewDate = var.ReviewDate
                    });
                }
                foreach (DTO.ReviewDTO var in newdtoReviews)
                {
                    rating += var.Rating;
                    count++;
                }
                rating = rating / count;
                context.SetMovieRating((int)(reviewDto.MovieId), rating);



                return Ok(dtoReview);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

                [HttpPost("editmovie")]
        public IActionResult EditMovie([FromBody] DTO.MovieDTO movieDto)
        {
            try
            {
                string? userEmail = HttpContext.Session.GetString("loggedInUser");
                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized("User is not logged in");
                }

                //Create model movie class
                Models.Movie modelsMovie = movieDto.GetModels();

                List<DTO.GenreDTO> genres = new List<DTO.GenreDTO>();
                foreach (Genre genre in modelsMovie.Genres)
                {
                    DTO.GenreDTO DTOGenre = new DTO.GenreDTO()
                    {
                        GenreId = genre.GenreId,
                        GenreName = genre.GenreName
                    };
                    genres.Add(DTOGenre);
                }


                context.SetMovieName(movieDto.MovieId, modelsMovie.MovieName);
                context.SetMovieReleaseYear(movieDto.MovieId, modelsMovie.ReleaseYear);
                context.SetMovieLenth(movieDto.MovieId, modelsMovie.Length);
                context.SetMovieImage(movieDto.MovieId, modelsMovie.Image);
                context.SetMovieTrailer(movieDto.MovieId, modelsMovie.Trailer);
                context.SetMovieDescription(movieDto.MovieId, modelsMovie.Description);
                context.SetMovieRating(movieDto.MovieId, modelsMovie.Rating);
                context.SetMovieDirector(movieDto.MovieId, modelsMovie.Director);
                context.SetMovieStar(movieDto.MovieId, modelsMovie.Star);
                context.SetMovieWriter(movieDto.MovieId, modelsMovie.Writer);
                context.SetMovieMultiDirectors(movieDto.MovieId, modelsMovie.MultiDirectors);
                context.SetMovieMultiStars(movieDto.MovieId, modelsMovie.MultiStars);
                context.SetMovieMultiWriters(movieDto.MovieId, modelsMovie.MultiWriters);
                context.SetMovieGenres(movieDto.MovieId, genres);
                context.SaveChanges();


                Models.Movie? updatedMovie = context.Movies.Include(m => m.Genres).Include(m => m.Reviews).FirstOrDefault(m => m.MovieId == movieDto.MovieId);

                if (updatedMovie == null)
                {
                    return NotFound("Movie not found after update");
                }

                // Create DTO from the UPDATED movie with all relationships loaded
                DTO.MovieDTO dtoMovie = new DTO.MovieDTO()
                {
                    MovieId = updatedMovie.MovieId,
                    MovieName = updatedMovie.MovieName,
                    ReleaseYear = updatedMovie.ReleaseYear,
                    Length = updatedMovie.Length,
                    Description = updatedMovie.Description,
                    Rating = updatedMovie.Rating,
                    Image = updatedMovie.Image,
                    Trailer = updatedMovie.Trailer,
                    Director = updatedMovie.Director,
                    Star = updatedMovie.Star,
                    Writer = updatedMovie.Writer,
                    MultiDirectors = updatedMovie.MultiDirectors,
                    MultiStars = updatedMovie.MultiStars,
                    MultiWriters = updatedMovie.MultiWriters,
                    Genres = new List<DTO.GenreDTO>(),
                    Reviews = new List<DTO.ReviewDTO>()
                };

                // Add the genres from the updated movie
                foreach (Genre genre in updatedMovie.Genres)
                {
                    dtoMovie.Genres.Add(new DTO.GenreDTO()
                    {
                        GenreId = genre.GenreId,
                        GenreName = genre.GenreName
                    });
                }

                // Add the reviews from the updated movie
                foreach (Review review in updatedMovie.Reviews)
                {
                    dtoMovie.Reviews.Add(new DTO.ReviewDTO()
                    {
                        ReviewId = review.ReviewId,
                        UserId = review.UserId,
                        MovieId = review.MovieId,
                        Rating = review.Rating,
                        Description = review.Description,
                        ReviewDate = review.ReviewDate
                    });
                }

                return Ok(dtoMovie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }





        //Get api/getusers
        //This method is used to get all users from the database and return a list of DTO.Users
        [HttpGet("getusers")]
        public IActionResult GetUsers()
        {
            try
            {
                List<DTO.UserDTO> dtoUsers = new List<DTO.UserDTO>();
                List<User> modelusers = context.Users.ToList();
                foreach (User var in modelusers)
                {
                    dtoUsers.Add(new DTO.UserDTO()
                    {
                        UserID = var.UserId,
                        Username = var.Username,
                        Password = var.Password,
                        Firstname = var.Firstname,
                        Lastname = var.Lastname,
                        Email = var.Email,
                        IsAdmin = var.IsAdmin,
                    });
                    //Get the profile image virtual path
                    string virtualPath = GetProfileImageVirtualPath(var.UserId);
                    dtoUsers.Last().ProfileImagePath = virtualPath;
                }
                return Ok(dtoUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("edituser")]
        public IActionResult EditUser([FromBody] DTO.UserDTO userDto)
        {
            try
            {
                //Check if who is logged in
                string? userEmail = HttpContext.Session.GetString("loggedInUser");
                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized("User is not logged in");
                }

                //Get model user class from DB with matching email. 
                Models.User? user = context.GetUser(userEmail);
                //Clear the tracking of all objects to avoid double tracking
                context.ChangeTracker.Clear();

                Models.User User = userDto.GetModels();

                context.Entry(User).State = EntityState.Modified;

                context.SaveChanges();

                //Task was updated!
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("promoteuser")]
        public IActionResult PromoteUser([FromBody] int id)
        {
            try
            {
                //Check if who is logged in
                string? userEmail = HttpContext.Session.GetString("loggedInUser");
                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized("User is not logged in");
                }

                //Get model user class from DB with matching id. 
                Models.User? user = context.GetUser(id);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                //Clear the tracking of all objects to avoid double tracking
                context.ChangeTracker.Clear();

                user.IsAdmin = true;
                context.Users.Update(user);
                context.SaveChanges();


                //Task was updated!
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("deleteuser")]
        public IActionResult DeleteUser([FromBody] int id)
        {
            try
            {
                //Check if who is logged in
                string? userEmail = HttpContext.Session.GetString("loggedInUser");
                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized("User is not logged in");
                }

                //Get model user class from DB with matching id. 
                Models.User? user = context.GetUser(id);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                //Clear the tracking of all objects to avoid double tracking
                context.ChangeTracker.Clear();

                context.RemoveUserReviews(id);

                context.Users.Remove(user);
                context.SaveChanges();

                //Task was updated!
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("UploadProfileImage")]
        public async Task<IActionResult> UploadProfileImageAsync(IFormFile file)
        {
            //Check if who is logged in
            string? userEmail = HttpContext.Session.GetString("loggedInUser");
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User is not logged in");
            }

            //Get model user class from DB with matching email. 
            Models.User? user = context.GetUser(userEmail);
            //Clear the tracking of all objects to avoid double tracking
            context.ChangeTracker.Clear();

            if (user == null)
            {
                return Unauthorized("User is not found in the database");
            }


            //Read all files sent
            long imagesSize = 0;

            if (file.Length > 0)
            {
                //Check the file extention!
                string[] allowedExtentions = { ".png", ".jpg" };
                string extention = "";
                if (file.FileName.LastIndexOf(".") > 0)
                {
                    extention = file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
                }
                if (!allowedExtentions.Where(e => e == extention).Any())
                {
                    //Extention is not supported
                    return BadRequest("File sent with non supported extention");
                }

                //Build path in the web root (better to a specific folder under the web root
                string filePath = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{user.UserId}{extention}";

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);

                    if (IsImage(stream))
                    {
                        imagesSize += stream.Length;
                    }
                    else
                    {
                        //Delete the file if it is not supported!
                        System.IO.File.Delete(filePath);
                    }

                }

            }

            DTO.UserDTO dtoUser = new DTO.UserDTO(user);
            dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.UserID);
            return Ok(dtoUser);
        }

        //this function gets a file stream and check if it is an image
        private static bool IsImage(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            List<string> jpg = new List<string> { "FF", "D8" };
            List<string> bmp = new List<string> { "42", "4D" };
            List<string> gif = new List<string> { "47", "49", "46" };
            List<string> png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
            List<List<string>> imgTypes = new List<List<string>> { jpg, bmp, gif, png };

            List<string> bytesIterated = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                string bit = stream.ReadByte().ToString("X2");
                bytesIterated.Add(bit);

                bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
                if (isImage)
                {
                    return true;
                }
            }

            return false;
        }

        //this function check which profile image exist and return the virtual path of it.
        //if it does not exist it returns the default profile image virtual path
        private string GetProfileImageVirtualPath(int userId)
        {
            string virtualPath = $"/profileImages/{userId}";
            string path = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{userId}.png";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".png";
            }
            else
            {
                path = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{userId}.jpg";
                if (System.IO.File.Exists(path))
                {
                    virtualPath += ".jpg";
                }
                else
                {
                    virtualPath = $"/profileImages/default.png";
                }
            }

            return virtualPath;
        }

        //THis function gets a userId and a profile image file and save the image in the server
        //The function return the full path of the file saved
        private async Task<string> SaveProfileImageAsync(int userId, IFormFile file)
        {
            //Read all files sent
            long imagesSize = 0;

            if (file.Length > 0)
            {
                //Check the file extention!
                string[] allowedExtentions = { ".png", ".jpg" };
                string extention = "";
                if (file.FileName.LastIndexOf(".") > 0)
                {
                    extention = file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
                }
                if (!allowedExtentions.Where(e => e == extention).Any())
                {
                    //Extention is not supported
                    throw new Exception("File sent with non supported extention");
                }

                //Build path in the web root (better to a specific folder under the web root
                string filePath = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{userId}{extention}";

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);

                    if (IsImage(stream))
                    {
                        imagesSize += stream.Length;
                    }
                    else
                    {
                        //Delete the file if it is not supported!
                        System.IO.File.Delete(filePath);
                        throw new Exception("File sent is not an image");
                    }

                }

                return filePath;

            }

            throw new Exception("File in size 0");
        }




    }
}
