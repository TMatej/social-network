using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Photo;
using DataAccessLayer.Entity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    public class PhotoService : GenericService<Photo>, IPhotoService
    {
        private readonly IMapper mapper;

        public PhotoService(IMapper mapper, IRepository<Photo> repository, IUnitOfWork uow) : base(repository, uow)
        {
            this.mapper = mapper;
        }

        public void UploadPhotoToGallery(PhotoInsertDTO photoDTO)
        {
            Insert(mapper.Map<Photo>(photoDTO));
        }
    }
}
