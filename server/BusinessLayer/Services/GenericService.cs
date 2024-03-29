﻿using Ardalis.GuardClauses;
using BusinessLayer.Contracts;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;


namespace BusinessLayer.Services
{
    /* CLASS SUITABLE ONLY FOR FACADE CALLS AND LATER REMAPPING INTO DTOs */
    public abstract class GenericService<TEntity> : IGenericService<TEntity>
        where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IUnitOfWork _uow;

        public GenericService(IRepository<TEntity> repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        public virtual void Delete(object id)
        {
            Guard.Against.Null(id);
            _repository.Delete(id);
            _uow.Commit();
        }

        public void Delete(TEntity entityToDelete)
        {
            Guard.Against.Null(entityToDelete);
            _repository.Delete(entityToDelete);
            _uow.Commit();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public TEntity GetByID(object id)
        {
            Guard.Against.Null(id);
            return _repository.GetByID(id);
        }

        public void Insert(TEntity entity)
        {
            Guard.Against.Null(entity);
            _repository.Insert(entity);
            _uow.Commit();
        }

        public void Update(TEntity entityToUpdate)
        {
            Guard.Against.Null(entityToUpdate);
            _repository.Update(entityToUpdate);
            _uow.Commit();
        }
    }
}
