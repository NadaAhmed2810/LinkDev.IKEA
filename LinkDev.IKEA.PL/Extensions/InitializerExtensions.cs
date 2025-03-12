using LinkDev.IKEA.DAL.contracts;

namespace LinkDev.IKEA.PL.Extensions
{
    public static class InitializerExtensions
    {
        public static void InitializerDatabase(this IApplicationBuilder app)
        {
            using var Scope = app.ApplicationServices.CreateScope();
            var services = Scope.ServiceProvider;
            var dbInitializer = services.GetRequiredService<IDbInitializer>();
            dbInitializer.Initialize();
            dbInitializer.seed();

        }
    }
}
