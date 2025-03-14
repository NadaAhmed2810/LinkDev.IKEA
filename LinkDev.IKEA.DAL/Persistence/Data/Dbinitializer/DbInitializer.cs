using LinkDev.IKEA.DAL.contracts;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Data.Dbinitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        public DbInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Initialize()
        {
            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                _dbContext.Database.Migrate();//Update database
            }
        }

        public void seed()
        {
            if (!_dbContext.Departments.Any())
            {
                var departmentData = File.ReadAllText("../LinkDev.IKEA.DAL/Persistence/Data/DataSeeds/departments.json");
                var departments = JsonSerializer.Deserialize<List<Department>>(departmentData);
                if (departments.Count() > 0)
                {
                    _dbContext.Departments.AddRange(departments);
                    _dbContext.SaveChanges();


                }
            }
        }
    }
}
