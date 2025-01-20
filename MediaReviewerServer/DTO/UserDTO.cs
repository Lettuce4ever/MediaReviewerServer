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
        public string Image {  get; set; }  

        public UserDTO()
        {

        }

        public UserDTO(Models.User user)
        {
            this.UserID = user.UserId;
            this.Username = user.Username;
            this.Password = user.Password;
            this.Firstname = user.Firstname;
            this.Lastname = user.Lastname;
            this.Email = user.Email;
            this.IsAdmin = user.IsAdmin;
            this.Image = user.Image;
        }

        public Models.User GetModels()
        {
            Models.User user = new Models.User();
            {
                UserID = this.UserID;
                Username = this.Username;
                Password = this.Password;
                Firstname = this.Firstname;
                Lastname = this.Lastname;
                Email = this.Email;
                IsAdmin = this.IsAdmin;
                Image = this.Image;
            }
            return user;
        }
    }
}
