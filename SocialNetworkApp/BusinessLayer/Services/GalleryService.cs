using AutoMapper;
using BusinessLayer.Config;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Gallery;
using BusinessLayer.DTOs.Gallery.Filters;
using BusinessLayer.DTOs.Photo;
using BusinessLayer.QueryObjects;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class GalleryService : GenericService<Gallery>, IGalleryService
    {
        protected readonly IMapper _mapper;
        protected readonly IRepository<Photo> _photoRepository;
        protected GalleryQueryObject _queryObject;

        public GalleryService(IRepository<Gallery> repository, 
            IRepository<Photo> photoRepository,
            IMapper mapper,
            GalleryQueryObject queryObject,
            IUnitOfWork uow) : base(repository, uow)
        {
            _mapper = mapper;
            _photoRepository = photoRepository;
            _queryObject = queryObject;
        }

        public GalleryRepresentDTO GetByIdWithListOfPhotos(int id)
        {
            /* somehow force DB to include also Photos during retrieving Gallery */
            var gallery = _queryObject.ExecuteQuery<int, GalleryRepresentDTO>(
                new GenericFilterDTO
                {
                    WhereColumnName = "Id",
                    FilterWhereExpression = (x => x == id),
                    IncludeParameters = new List<string>() { "Photos", "Profile" },
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
