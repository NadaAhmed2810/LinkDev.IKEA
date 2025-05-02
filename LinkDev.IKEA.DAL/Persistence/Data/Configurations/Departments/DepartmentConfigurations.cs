using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Persistance.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.IKEA.DAL.Persistance.Data.Configurations.Departments
{
    internal class DepartmentConfigurations : BaseAuditableConfiguration<int, Department>
    {
        public override void Configure(EntityTypeBuilder<Department> builder)
        {
            base.Configure(builder);
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).HasColumnType("varchar(100)").IsRequired(false);
            builder.Property(D => D.Code).HasColumnType("varchar(10)").IsRequired(false);
            builder.Property(D => D.Description).HasColumnType("varchar(100)");
            //Manager RelationShip
            builder.HasOne(D => D.Manager)
                   .WithOne()
                   .HasForeignKey<Department>(D => D.ManagerId)
                   .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
