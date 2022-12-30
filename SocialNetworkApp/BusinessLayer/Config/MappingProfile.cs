using BusinessLayer.DTOs.Comment;
using BusinessLayer.DTOs.FileEntity;
using BusinessLayer.DTOs.Gallery;
using BusinessLayer.DTOs.Message;
using BusinessLayer.DTOs.Photo;
using BusinessLayer.DTOs.Post;
using BusinessLayer.DTOs.Profile;
using BusinessLayer.DTOs.Search;
using BusinessLayer.DTOs.User;
using DataAccessLayer.Entity;

namespace BusinessLayer.Config
{
    public class MappingProfile : AutoMapper.Profile
    {

        public MappingProfile()
        {
            // Gallery 
            CreateMap<Gallery, GalleryBasicRepresentDTO>();
            CreateMap<Gallery, GalleryCreateDTO>().ReverseMap();
            CreateMap<Gallery, GalleryRepresentDTO>()
                .ForMember(dest => dest.Profile,
                    opt => opt.MapFrom(src => src.Profile));
            CreateMap<Gallery, GalleryWithProfileRepresentDTO>()
                .ForMember(dest => dest.Profile,
                    opt => opt.MapFrom(src => src.Profile));
            CreateMap<Gallery, GalleryWithPhotosRepresentDTO>();

            // Photo
            CreateMap<Photo, PhotoCreateDTO>().ReverseMap();
            CreateMap<Photo, PhotoRepresentDTO>().ReverseMap();

            // File 
            CreateMap<FileEntity, FileEntityDTO>().ReverseMap();
            CreateMap<FileEntity, FileStreamDTO>().ReverseMap();

            // Photo
            CreateMap<Photo, PhotoCreateDTO>().ReverseMap();
            CreateMap<Photo, PhotoRepresentDTO>().ReverseMap();
            
            // Comment
            CreateMap<Comment, CommentCreateDTO>().ReverseMap();
            CreateMap<Comment, CommentEditDTO>().ReverseMap();
            CreateMap<Comment, CommentRepresentDTO>().ReverseMap();
            CreateMap<Comment, CommentBasicRepresentDTO>().ReverseMap();
            
            // Profile 
            CreateMap<DataAccessLayer.Entity.Profile, ProfileBasicRepresentDTO>().ReverseMap();      
            
            // Post 
            CreateMap<PostCreateDTO, Post>().ReverseMap();
            CreateMap<Post, PostRepresentDTO>().ReverseMap();

            // Address
            CreateMap<Address, AddressDTO>().ReverseMap();

            // User 
            CreateMap<User, UserDTO>()
              .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.UserRoles.Select(r => r.Role.Name)))
              .ReverseMap();
            CreateMap<User, SearchResultDTO>()
              .ForMember(x => x.Type, opt => opt.MapFrom(x => x.GetType().Name))
              .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Username))
              .ForMember(x => x.Image, opt => opt.MapFrom(x => x.Avatar))
              .ReverseMap();

            // Group
            CreateMap<Group, SearchResultDTO>()
              .ForMember(x => x.Type, opt => opt.MapFrom(x => x.GetType().Name))
              .ReverseMap();

            // Event
            CreateMap<Event, SearchResultDTO>()
              .ForMember(x => x.Type, opt => opt.MapFrom(x => x.GetType().Name))
              .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Title))
              .ReverseMap();

            // Message
            CreateMap<Message, MessageCreateDTO>().ReverseMap();
            CreateMap<Message, MessageRepresentDTO>().ReverseMap();

        }
    }
}
