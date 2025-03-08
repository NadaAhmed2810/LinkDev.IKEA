using LinkDev.IKEA.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Data.Configurations.Departments
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10,10);
            builder.Property(D => D.Name).HasColumnType("varchar(100)");
            builder.Property(D => D.Code).HasColumnType("varchar(10)");
            builder.Property(D => D.Description).HasColumnType("varchar(100)");
            builder.Property(D => D.CreatedBy).HasColumnType("varchar(100)");
            builder.Property(D => D.LastModifiedBy).HasColumnType("varchar(100)");
            builder.Property(D => D.LastModifiedOn).HasDefaultValueSql("GetUTCDate()");
            builder.Property(D => D.CreatedOn).HasComputedColumnSql("GetUTCDate()");





        }
    }
}
