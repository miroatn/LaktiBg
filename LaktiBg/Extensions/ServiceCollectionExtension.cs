using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LaktiBg.Infrastructure.Data;
using LaktiBg.Infrastructure.Data.Common;
using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Services.EventServices;
using LaktiBg.Core.Contracts.PlaceServices;
using LaktiBg.Core.Services.PlaceServices;
using LaktiBg.Core.Contracts.User;
using LaktiBg.Core.Services.UserServices;
using LaktiBg.Core.Contracts;
using LaktiBg.Core.Services.CommentServices;
using LaktiBg.Core.Contracts.ImageService;
using LaktiBg.Core.Services.ImageServices;

namespace LaktiBg.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IImageService, ImageService>();

            return services;
        }
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<IRepository, Repository>();

            return services;
        }
        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }
    }
}
