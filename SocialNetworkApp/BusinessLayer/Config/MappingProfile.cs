using BusinessLayer.DTOs.Gallery;
using BusinessLayer.DTOs.Gallery.Results;
using BusinessLayer.DTOs.Photo;
using BusinessLayer.DTOs.Profile;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using Profile = AutoMapper.Profile;

namespace BusinessLayer.Config
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Gallery, GalleryBasicRepresentDTO>();
            CreateMap<GalleryCreateDTO, Gallery>(); 
            CreateMap<Photo, PhotoInsertDTO>().ReverseMap();
            CreateMap<Photo, PhotoRepresentDTO>().ReverseMap();
            CreateMap<QueryResult<Gallery>, QueryResultDto<GalleryBasicRepresentDTO>>().ReverseMap();
            CreateMap<DataAccessLayer.Entity.Profile, ProfileBasicRepresentDTO>().ReverseMap();
            CreateMap<Gallery, GalleryRepresentDTO>()
                .ForMember(dest => dest.Profile,
                    opt => opt.MapFrom(src => src.Profile));
            CreateMap<QueryResult<Gallery>, QueryResultDto<GalleryRepresentDTO>>().ReverseMap();
        }
    }
}
