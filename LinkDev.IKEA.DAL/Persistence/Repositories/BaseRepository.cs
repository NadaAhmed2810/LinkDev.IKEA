using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinkDev.IKEA.DAL.Common.Entities;
using LinkDev.IKEA.DAL.Contracts.Repositories;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.DAL.Persistence.Repositories
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
        
        public TEntity? GetById(TKey id) => _dbSet.Find(id);
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if(withTracking)
            {
                return _dbSet.ToList();
            }
            else
            {
                return _dbSet.AsNoTracking().ToList();
            }
        }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }
        public void Update(TEntity entity)
        {
           _dbSet.Update(entity);
        }

        public void Delete(TKey id)
        {
            var entity = _dbSet.Find(id);
            if (entity is { })
            {
                _dbSet.Remove(entity);
            }
        }
         public TEntity? Get(Expression<Func<TEntity, bool>> filter,Func<IQueryable<TEntity>,IQueryable<TEntity>>? Include=null)
        {
           IQueryable<TEntity> query = _dbSet;//_dbset.Employees.Includes().Where(filter);
            if (Include is { })
            {
                query = Include(query);
            }
            query= query.Where(filter);
            return query.FirstOrDefault();
        }
   

      

        public PaginatedResult<TEntity> GetAll(QueryParameters queryParameters, Expression<Func<TEntity, bool>>? filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? Includes = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (Includes is { })
            {
                query = Includes(query);
            }
            if(filter is { })
               query = query.Where(filter);
            //Get total count before applying pagination
            var TotalCount = query.Count();
            if (OrderBy is { })
            {
                query = OrderBy(query);
            }
            var items = query.Skip(queryParameters.PageSize*(queryParameters.PageIndex - 1)).Take(queryParameters.PageSize).ToList();
            return new PaginatedResult<TEntity>
            {
                Data = items,
                TotalCount = TotalCount,
                PageIndex = queryParameters.PageIndex,
                PageSize = queryParameters.PageSize,
            };

        }
        public bool Exists(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Any(filter);
        }
    }
}
