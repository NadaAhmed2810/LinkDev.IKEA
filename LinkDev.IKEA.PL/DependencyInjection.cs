using System.Reflection;
using LinkDev.IKEA.DAL.Entities.Identity;
using LinkDev.IKEA.DAL.Persistance.Data;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.IKEA.PL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebService(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
         
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;//@$%^&*_/><
                options.Password.RequiredUniqueChars = 1;//P@ssw0rd
                                                         
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
               
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;

        }
    }
}
