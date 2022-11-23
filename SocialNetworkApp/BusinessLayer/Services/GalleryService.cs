using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Photo;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    public class GalleryService : GenericService<Gallery>, IGalleryService
    {
        protected readonly IMapper _mapper;
        protected readonly IRepository<Photo> _photoRepository;
        protected readonly IQuery<Gallery> _galleryQuery;

        public GalleryService(IRepository<Gallery> repository,
            IRepository<Photo> photoRepository,
            IQuery<Gallery> galleryQuery,
            IMapper mapper,
            IUnitOfWork uow) : base(repository, uow)
        {
            _mapper = mapper;
            _photoRepository = photoRepository;
            _galleryQuery = galleryQuery;
        }

        public Gallery GetByIdWithListOfPhotos(int id)
        {
            var gallery = _galleryQuery
                .Where<int>(x => x == id, "Id")
                .Page(1, 10)
                .Include("Photos")
                .Execute();

            return gallery.Items.First();
        }

        public void UploadPhotoToGallery(PhotoInsertDTO photoDTO, int galleryId)
        {
            var mapped = _mapper.Map<Photo>(photoDTO);
            mapped.GaleryId = galleryId;
            _photoRepository.Insert(mapped);
            _uow.Commit();
        }
    }
}
