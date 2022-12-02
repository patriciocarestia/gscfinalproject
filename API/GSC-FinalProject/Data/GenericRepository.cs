using GSC_FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GSC_FinalProject.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : EntityBase
    {
        private readonly DataContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DataContext context) 
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public List<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity GetById(int id)
        {
            return _dbSet.SingleOrDefault(entity => entity.Id == id)!;
        }
        public bool Exists(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Any(filter);
        }

        public TEntity Add(TEntity entity)
        {
            var savedEntity = _dbSet.Add(entity);
            return savedEntity.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            var updatedEntity = _dbSet.Update(entity);
            return updatedEntity.Entity;
        }

        public void Delete(int id)
        {
            var removedEntity = _dbSet.Find(id);
            _dbSet.Remove(removedEntity!);
        }
    }
}
