using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinkDev.IKEA.DAL.Common.Entities;
using LinkDev.IKEA.DAL.Persistence.Common;

namespace LinkDev.IKEA.DAL.Contracts.Repositories
{
    public interface IBaseRepository<TEntity,TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
    
        IEnumerable<TEntity> GetAll(bool withTracking = false);
        TEntity? GetById(TKey id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey id);
        PaginatedResult<TEntity> GetAll(QueryParameters queryParameters,
                                          Expression<Func<TEntity, bool>> filter
                                         ,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderBy = null,
                                          Func<IQueryable<TEntity>, IQueryable<TEntity>>? Includes = null);
        TEntity? Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IQueryable<TEntity>>? Include = null);
        bool Exists(Expression<Func<TEntity, bool>> filter);
    }
  
}
