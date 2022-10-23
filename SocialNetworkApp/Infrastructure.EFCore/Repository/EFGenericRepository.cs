﻿using DataAccessLayer;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.Repository
{
    public class EFGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal SocialNetworkDBContext context;
        internal DbSet<TEntity> dbSet;

        public EFGenericRepository(SocialNetworkDBContext dbcontext)
        {
            context = dbcontext;
            dbSet = context.Set<TEntity>();
        }

        public virtual List<TEntity> GetAll()
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
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
