using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Post;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Profile = DataAccessLayer.Entity.Profile;

namespace BusinessLayer.Services
{
    public class ProfileService : GenericService<Profile>, IProfileService
    {
        private IMapper mapper;
        private IPostService postService;
        private IQuery<Profile> profileQuery;

        public ProfileService(IQuery<Profile> profileQuery, IRepository<Profile> repository, IPostService postService, IUnitOfWork uow, IMapper mapper) : base(repository, uow)
        {
            this.mapper = mapper;
            this.postService = postService;
            this.profileQuery = profileQuery;
        }

        public void addPost(int profileId, int userId, PostCreateDTO postDTO)
        {
            Post post = mapper.Map<Post>(postDTO);
            post.UserId = userId;
            post.PostableId = profileId;

            postService.Insert(post);
        }

        public Profile getByUserId(int userId)
        {
            var profile = profileQuery.Where<int>(id => id == userId, "UserId").Include("User").Execute().Items.FirstOrDefault();

            if (profile == null)
            {
              throw new Exception("Profile not found");
            }

            return profile;
        }
    }
}
