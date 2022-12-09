using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Photo;
using BusinessLayer.DTOs.Query;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using BusinessLayer.DTOs.Gallery;
using Ardalis.GuardClauses;

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

        public GalleryWithPhotosRepresentDTO GetByIdWithPhotos(int id)
        {
            var gallery = _galleryQuery
                .Where<int>(x => x == id, "Id")
                .Include("Photos")
                .Execute();

            return gallery.Items.FirstOrDefault();
        }

        public GalleryWithProfileRepresentDTO GetByIdWithProfile(int id)
        {
            var gallery = _galleryQuery
                .Where<int>(x => x == id, "Id")
                .Include("Profile")
                .Execute();

            return gallery.Items.FirstOrDefault();
        }

        public GalleryRepresentDTO GetByIdDetailed(int id)
        {
            var gallery = _galleryQuery
                .Where<int>(x => x == id, "Id")
                .Include("Profile", "Photos")
                .Execute();

            return gallery.Items.FirstOrDefault();
        }

        public void UploadPhotoToGallery(PhotoInsertDTO photoDTO, int galleryId)
        {
            Guard.Against.Null(photoDTO);

            var mapped = _mapper.Map<Photo>(photoDTO);
            mapped.GaleryId = galleryId;
            _photoRepository.Insert(mapped);
            _uow.Commit();
        }
    }
}
