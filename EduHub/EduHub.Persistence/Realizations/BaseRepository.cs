using System.Linq.Expressions;
using EduHub.Persistence.Abstractions;
using EduHub.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace EduHub.Persistence.Realizations
{
    public class BaseRepository<TEntity> : IRepositoryAsync<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<TEntity> dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null,
            int skip = 0,
            int take = int.MaxValue)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();

            queryable = filter is null ? queryable : queryable.Where(filter);
            queryable = orderBy is null ? queryable : orderBy(queryable);
            queryable = queryable.Skip(skip).Take(take);
            queryable = includes is null ? queryable : includes(queryable);

            return await queryable.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetFromSqlRowAsync(
            string sqlRaw,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int skip = 0,
            int take = int.MaxValue
        )
        {
            var queryable = _context.Set<TEntity>().FromSqlRaw(sqlRaw);
            queryable = orderBy is null ? queryable : orderBy(queryable);
            queryable = queryable.Skip(skip).Take(take);
            queryable = includes is null ? queryable : includes(queryable);

            return await queryable.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();
            queryable = filter is null ? queryable : queryable.Where(filter);
            queryable = includes is null ? queryable : includes(queryable);


            return await queryable.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();
            queryable = filter is null ? queryable : queryable.Where(filter);

            return await queryable.CountAsync();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public void Update(TEntity entityToUpdate)
        {
            _context.Set<TEntity>().Update(entityToUpdate);
        }

        public void Delete(TEntity entityToDelete)
        {
            _context.Set<TEntity>().Remove(entityToDelete);
        }

        public void Delete(IEnumerable<TEntity> entityToDelete)
        {
            _context.Set<TEntity>().RemoveRange(entityToDelete);
        }
    }
}