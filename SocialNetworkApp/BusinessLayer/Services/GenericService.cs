using AutoMapper;
using BusinessLayer.Contracts;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;


namespace BusinessLayer.Services
{
    public abstract class GenericService<TEntity> : IGenericService<TEntity> 
        where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _uow;

        public GenericService(IRepository<TEntity> repository, IMapper mapper, IUnitOfWork uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete<TDTO>(TDTO entityToDelete) where TDTO : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TDTO> GetAll<TDTO>() where TDTO : class
        {
            throw new NotImplementedException();
        }

        public TDTO GetByID<TDTO>(object id) where TDTO : class
        {
            var myObject = _repository.GetByID(id);
            // for some reason returns Gallery.Profile = null
            return _mapper.Map<TDTO>(myObject);
        }

        public void Insert<TDTO>(TDTO entity) where TDTO : class
        {
            throw new NotImplementedException();
        }

        public void Update<TDTO>(TDTO entityToUpdate) where TDTO : class
        {
            throw new NotImplementedException();
        }
    }
}
