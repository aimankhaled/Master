using POS.Shared.Helpers;
using System.Linq.Expressions;

namespace POS.Server.Domain
{
    public interface IRepository<TEntity> where TEntity : class

    {
       Task<IEnumerable<TEntity>> All(bool getDeleted = false);
      
        Task Insert(TEntity entity);
        Task Update(TEntity updatedEntity);

        Task<RepositertyReturn<IEnumerable<TEntity>>> FindByInclude
        (Expression<Func<TEntity, bool>> predicate,
            string includeProperties);
       
      
        Task<IEnumerable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate);
    }
}
