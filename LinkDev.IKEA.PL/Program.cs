using LinkDev.IKEA.DAL;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.PL.Extensions;
using Microsoft.EntityFrameworkCore;
using LinkDev.IKEA.BLL;
namespace LinkDev.IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Configure Services

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<ApplicationDbContext>(serviceProvider =>
            //{
            //    //var options=serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();
            //    var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //    optionBuilder.UseSqlServer("Server=.;Database=IKEA;Trusted_Connection=True");
            //    return new ApplicationDbContext(optionBuilder.Options);
            //});
            builder.Services.AddDbContext<ApplicationDbContext>(
                contextLifetime: ServiceLifetime.Scoped,
                optionsLifetime: ServiceLifetime.Scoped,
                optionsAction: (optionsBuilder) =>
                {
                    optionsBuilder.UseSqlServer("Server=.;Database=IKEA;Trusted_Connection=True");
                }
                );
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddLogging();

            #endregion

            var app = builder.Build();

            #region Initialize Databse 
            InitializerExtensions.InitializerDatabase(app);

            #endregion





            #region Configure HTTP Request Pipelines
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseRouting();


            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets(); 
            #endregion

            app.Run();
        }
    }
}
