using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Photo;
using BusinessLayer.DTOs.Query;
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

        public Gallery GetByIdWithPhotos(int id)
        {
            var gallery = _galleryQuery
                .Where<int>(x => x == id, "Id")
                .Include("Photos")
                .Execute();

            return gallery.Items.FirstOrDefault();
        }

        public Gallery GetByIdWithProfile(int id)
        {
            var gallery = _galleryQuery
                .Where<int>(x => x == id, "Id")
                .Include("Profile")
                .Execute();

            return gallery.Items.FirstOrDefault();
        }

        public Gallery GetByIdDetailed(int id)
        {
            var gallery = _galleryQuery
                .Where<int>(x => x == id, "Id")
                .Include("Profile", "Photos")
                .Execute();

            return gallery.Items.FirstOrDefault();
        }
    }
}
