using AutoMapper;
using BusinessLayer.Config;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Query;
using BusinessLayer.DTOs.Query.Filters;
using BusinessLayer.DTOs.Photo;
using BusinessLayer.QueryObjects;
using DataAccessLayer.Entity;
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
        protected GenericQueryObject<Gallery> _queryObject;

        public GalleryService(IRepository<Gallery> repository, 
            IRepository<Photo> photoRepository,
            IMapper mapper,
            GenericQueryObject<Gallery> queryObject,
            IUnitOfWork uow) : base(repository, uow)
        {
            _mapper = mapper;
            _photoRepository = photoRepository;
            _queryObject = queryObject;
        }

        public GalleryWithPhotosRepresentDTO GetByIdWithPhotos(int id)
        {
            var gallery = _queryObject
                .ApplyWhereClause(
                new GenericWhereDTO<int>
                {
                    WhereColumnName = "Id",
                    FilterWhereExpression = (x => x == id),
                })
                .ExecuteQuery<GalleryWithPhotosRepresentDTO>(
                new GenericFilterDTO
                { 
                    IncludeParameters = new List<string>() { "Photos" },
                    RequestedPageNumber = 1,
                    RequestedPageSize = 10
                });

            return gallery.Items.First();
        }

        public GalleryWithProfileRepresentDTO GetByIdWithProfile(int id)
        {
            var gallery = _queryObject
                .ApplyWhereClause(
                new GenericWhereDTO<int>
                {
                    WhereColumnName = "Id",
                    FilterWhereExpression = (x => x == id),
                })
                .ExecuteQuery<GalleryWithProfileRepresentDTO>(
                new GenericFilterDTO
                {
                    IncludeParameters = new List<string>() { "Profile" },
                    RequestedPageNumber = 1,
                    RequestedPageSize = 10
                });

            return gallery.Items.First();
        }

        public GalleryRepresentDTO GetByIdDetailed(int id)
        {
            var gallery = _queryObject
                .ApplyWhereClause(
                new GenericWhereDTO<int>
                {
                    WhereColumnName = "Id",
                    FilterWhereExpression = (x => x == id),
                })
                .ExecuteQuery<GalleryRepresentDTO>(
                new GenericFilterDTO
                {
                    IncludeParameters = new List<string>() { "Photos", "Profile" },
                    RequestedPageNumber = 1,
                    RequestedPageSize = 10
                });

            return gallery.Items.First();
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
