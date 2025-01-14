using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediaReviewerServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Data.SqlClient;
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

    }
}
