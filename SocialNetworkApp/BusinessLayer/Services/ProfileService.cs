using AutoMapper;
using BusinessLayer.DTOs.Post;
using DataAccessLayer.Entity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Profile = DataAccessLayer.Entity.Profile;

namespace BusinessLayer.Services
{
    internal class ProfileService : GenericService<Profile>
    {
        private IMapper mapper;
        private PostService postService;

        public ProfileService(IRepository<Profile> repository, PostService postService, IUnitOfWork uow, IMapper mapper) : base(repository, uow)
        {
            this.mapper = mapper;
            this.postService = postService;
        }

        public void addPost(Profile profile, int userId, PostCreateDTO postDTO)
        {
            Post post = mapper.Map<Post>(postDTO);
            post.UserId = userId;
            post.PostableId = profile.Id;

            postService.Insert(post);
        }
    }
}
