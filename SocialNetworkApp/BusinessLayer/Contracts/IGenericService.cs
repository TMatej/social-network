﻿namespace BusinessLayer.Contracts
{
    public interface IGenericService<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetByID(object id);

        void Insert(TEntity entity);
        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}
