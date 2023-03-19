using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(
                options => options.UseNpgsql(config.GetConnectionString("DefaultConnection"))
            );
            services.AddScoped<ITokenService, TokenService>();
            services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService, PhotoService>();
            return services;
        }
    }
}