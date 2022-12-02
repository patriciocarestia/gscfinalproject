using GSC_FinalProject.Entities;
using System.Linq.Expressions;

namespace GSC_FinalProject.Data
{
    public interface IGenericRepository<TEntity>
        where TEntity : EntityBase
    {
        List<TEntity> GetAll();
        TEntity GetById(int id);
        bool Exists(Expression<Func<TEntity, bool>> filter);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(int id);

    }
}
