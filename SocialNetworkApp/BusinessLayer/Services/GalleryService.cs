using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Query;
using BusinessLayer.DTOs.Query.Filters;
using BusinessLayer.DTOs.Photo;
using BusinessLayer.QueryObjects;
using DataAccessLayer.Entity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

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

        public GalleryRepresentDTO GetByIdWithListOfPhotos(int id)
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
                    IncludeParameters = new List<string>() { "Photos" },
                    RequestedPageNumber = 1,
                    RequestedPageSize = 10
                });

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
