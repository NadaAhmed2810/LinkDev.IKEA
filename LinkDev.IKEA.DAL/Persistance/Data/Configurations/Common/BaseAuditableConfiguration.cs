using LinkDev.IKEA.DAL.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Data.Configurations.Common
{
    internal class BaseAuditableConfiguration<TKey,TEntity> : BaseEntityConfiguration<TKey,TEntity>
        where TKey : IEquatable<TKey>
        where  TEntity: BaseAuditableEntity<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
            builder.Property(D => D.CreatedBy).HasColumnType("varchar(100)");
            builder.Property(D => D.LastModifiedBy).HasColumnType("varchar(100)");
            builder.Property(D => D.LastModifiedOn).HasDefaultValueSql("GetUTCDate()");
            builder.Property(D => D.CreatedOn).HasComputedColumnSql("GetUTCDate()");


        }
    }
}
