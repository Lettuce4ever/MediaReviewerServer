            using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MediaReviewerServer.Models;
namespace MediaReviewerServer.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string ProfileImagePath { get; set; } = "";
        public UserDTO()
        {

        }

        public UserDTO(Models.User modelUser)
        {
            this.UserID = modelUser.UserId;
            this.Username = modelUser.Username;
            this.Password = modelUser.Password;
            this.Firstname = modelUser.Firstname;
            this.Lastname = modelUser.Lastname;
            this.Email = modelUser.Email;
            this.IsAdmin = modelUser.IsAdmin;
        }

        public Models.User GetModels()
        {
            Models.User modelUser = new Models.User()
            {
                UserId = this.UserID,
                Username = this.Username,
                Password = this.Password,
                Firstname = this.Firstname,
                Lastname = this.Lastname,
                Email = this.Email,
                IsAdmin = this.IsAdmin,
            };
            return modelUser;
        }
    }
}
