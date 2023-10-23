using Microsoft.EntityFrameworkCore;
using POS.Server.Domain;
using POS.Shared.Helpers;
using System.Linq.Expressions;

namespace POS.Server.InfaStructure
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DbContext _context;
        internal DbSet<TEntity> _dbSet;
        private ILogger<EfRepository<TEntity>> _logger;
        public EfRepository(DbContext context, ILogger<EfRepository<TEntity>> logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> All(bool getDeleted = false)
        {
            return await _dbSet.ToListAsync();
        }

       
        public async Task<IEnumerable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var query = _dbSet
                    .Where(predicate);

                IEnumerable<TEntity> results = await query.ToListAsync();
                return results;
            }
            catch (Exception ex)
            {
                IEnumerable<TEntity> results = await _dbSet
                    .Where(predicate).ToListAsync();
                return results;
            }
        }
       
        public async Task Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.ChangeTracker.DetectChanges();
            _logger.LogDebug(_context.ChangeTracker.DebugView.LongView);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity updatedEntity)
        {
            await _context.SaveChangesAsync();
        }
        
      
        public async Task<RepositertyReturn<IEnumerable<TEntity>>> FindByInclude
        (Expression<Func<TEntity, bool>> predicate,
            string includeProperties)
        {
            var query = GetAllIncluding(includeProperties);

            query = query.Where(predicate);
            RepositertyReturn<IEnumerable<TEntity>> ret = new RepositertyReturn<IEnumerable<TEntity>>();
            ret.Count = await query.CountAsync();

            ret.Data = await query.ToListAsync();
            return ret;
        }
        private IQueryable<TEntity> GetAllIncluding
             (string includePropertiesString)
        {
            var includeProperties = includePropertiesString.Split(',');
            IQueryable<TEntity> queryable = _dbSet;
            var query = includeProperties.Aggregate
                (queryable, (current, includeProperty) => current.Include(includeProperty));
            return query;
        }

    }
}