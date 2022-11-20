using DataAccessLayer.Entity;
using Infrastructure.EFCore.UnitOfWork;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.Repository
{
    public class EFGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        internal EFUnitOfWork uow;
        internal DbSet<TEntity> dbSet;

        public EFGenericRepository(EFUnitOfWork uow)
        {
            this.uow = uow;
            dbSet = uow.Context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (uow.Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            uow.Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            uow.Context.SaveChanges();
        }
    }
}
