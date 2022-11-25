using BusinessLayer.DTOs.Photo;
using BusinessLayer.DTOs.Post;
using BusinessLayer.DTOs.Profile;
using BusinessLayer.DTOs.Query;
using DataAccessLayer.Entity;
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
            CreateMap<DataAccessLayer.Entity.Profile, ProfileBasicRepresentDTO>().ReverseMap();
            CreateMap<Gallery, GalleryRepresentDTO>()
                .ForMember(dest => dest.Profile,
                    opt => opt.MapFrom(src => src.Profile));
            CreateMap<PostCreateDTO, Post>().ReverseMap();
        }
    }
}