using LinkDev.IKEA.DAL.contracts;
using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistence.Data.Dbinitializer;
using LinkDev.IKEA.DAL.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString: configuration.GetConnectionString("DefaultConnection"));
            }
            );
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOFWork, UnitOfWork>();
            return services;
        } 
    }
}
