using MediaReviewerServer.DTO;
using MediaReviewerServer.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaReviewerServer.Models
{
    public partial class MediaReviewerDbContext : DbContext
    {
        public User? GetUser(string email)
        {
            return this.Users.Where(u => u.Email == email).FirstOrDefault();
        }
    }
}
