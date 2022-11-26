using BusinessLayer.DTOs.Comment;
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
            /* Gallery */
            CreateMap<Gallery, GalleryBasicRepresentDTO>();
            CreateMap<GalleryCreateDTO, Gallery>();
            CreateMap<Gallery, GalleryRepresentDTO>()
                .ForMember(dest => dest.Profile,
                    opt => opt.MapFrom(src => src.Profile));
            
            /* Photo */
            CreateMap<Photo, PhotoInsertDTO>().ReverseMap();
            CreateMap<Photo, PhotoRepresentDTO>().ReverseMap();
            
            /* Comment */
            CreateMap<Comment, CommentCreateDTO>().ReverseMap();
            CreateMap<Comment, CommentEditDTO>().ReverseMap();
            CreateMap<Comment, CommentRepresentDTO>().ReverseMap();
            CreateMap<Comment, CommentBasicRepresentDTO>().ReverseMap();
            
            /* Profile */
            CreateMap<DataAccessLayer.Entity.Profile, ProfileBasicRepresentDTO>().ReverseMap();      
            
            /* Post */
            CreateMap<PostCreateDTO, Post>().ReverseMap();  
        }
    }
}