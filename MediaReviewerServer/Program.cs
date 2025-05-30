using MediaReviewerServer.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaReviewerServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            #region Add Database context to Dependency Injection
            //Read connection string from app settings.json
            string connectionString = builder.Configuration
                .GetSection("ConnectionStrings")
                .GetSection("MediaReviewerDB").Value;

            //Add Database to dependency injection
            builder.Services.AddDbContext<MediaReviewerDbContext>(
                    options => options.UseSqlServer(connectionString));

            #endregion

            #region Add Session
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = true;
            });
            #endregion

            #region for debugginh UI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #endregion

            var app = builder.Build();

            #region for debugginh UI
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            #endregion

            #region Add Session
            app.UseSession(); //In order to enable session management
            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles(); //Support static files delivery from wwwroot folder
            // Configure the application to listen on all network interfaces
            // Also note the changes in launchSettings.json
            // And you should press "allow" when windows firewall prompte a message after running the server for the first time!
            //app.Urls.Add("http://*:5110");
            //app.Urls.Add("https://*:7012");
            app.MapControllers(); //Map all controllers classes

            
            app.Run();
        }
    }
}
