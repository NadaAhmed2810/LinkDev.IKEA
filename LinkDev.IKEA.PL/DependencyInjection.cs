using System.Reflection;

namespace LinkDev.IKEA.PL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebService(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;

        }
    }
}
