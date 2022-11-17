using BusinessLayer.DTOs.Galery;
using DataAccessLayer.Entity;
using Profile = AutoMapper.Profile;

namespace BusinessLayer.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Gallery, GalleryRepresentDTO>()
                //.ForMember(dest => dest.PhotosCount, cfg => cfg.MapFrom(src => src.Photos.Count))
                .ForMember(dest => dest.ProfileName, cfg => cfg.MapFrom(src => src.Profile.Name))
                .ForMember(dest => dest.UserId, cfg => cfg.MapFrom(src => src.Profile.UserId))
                .ForMember(dest => dest.UserName, cfg => cfg.MapFrom(src => src.Profile.User.Username));
        }
    }
}
