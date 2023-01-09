using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Photo;
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
        protected readonly IFileService _fileService;

        public GalleryService(IRepository<Gallery> repository,
            IRepository<Photo> photoRepository,
            IQuery<Gallery> galleryQuery,
            IMapper mapper,
            IFileService fileService,
            IUnitOfWork uow) : base(repository, uow)
        {
            _mapper = mapper;
            _photoRepository = photoRepository;
            _fileService = fileService;
            _galleryQuery = galleryQuery;
        }

        public GalleryWithPhotosRepresentDTO GetByIdWithPhotos(int id)
        {
            var gallery = _galleryQuery
                .Where<int>(x => x == id, "Id")
                .Include("Photos")
                .Execute();

            return _mapper.Map<GalleryWithPhotosRepresentDTO>(gallery.Items.FirstOrDefault());
        }

        public GalleryWithProfileRepresentDTO GetByIdWithProfile(int id)
        {
            var gallery = _galleryQuery
                .Where<int>(x => x == id, "Id")
                .Include("Profile")
                .Execute();

            return _mapper.Map<GalleryWithProfileRepresentDTO>(gallery.Items.FirstOrDefault());
        }

        public GalleryRepresentDTO GetByIdDetailed(int id)
        {
            var gallery = _galleryQuery
                .Where<int>(x => x == id, "Id")
                .Include(nameof(Gallery.Profile), nameof(Gallery.Photos), $"{nameof(Gallery.Photos)}.{nameof(Photo.FileEntity)}")
                .Execute();

            return _mapper.Map<GalleryRepresentDTO>(gallery.Items.FirstOrDefault());
        }

        public IEnumerable<GalleryRepresentDTO> GetGalleriesByProfileId(int profileId)
        {
            var galleries = _galleryQuery
                .Where<int>(x => x == profileId, nameof(Gallery.ProfileId))
                .Include(nameof(Gallery.Profile), nameof(Gallery.Photos), $"{nameof(Gallery.Photos)}.{nameof(Photo.FileEntity)}")
                .Execute();

            return galleries.Items.Select(gallery => _mapper.Map<GalleryRepresentDTO>(gallery));
        }

        public void UploadPhotoToGallery(PhotoCreateDTO photoDTO, int galleryId)
        {
            Guard.Against.Null(photoDTO);

            var photo = _mapper.Map<Photo>(photoDTO);
            var file = _fileService.CreateFile(photoDTO.File); 

            photo.GalleryId = galleryId;
            photo.FileEntity = file;
            _photoRepository.Insert(photo);
            _uow.Commit();
        }
    }
}
