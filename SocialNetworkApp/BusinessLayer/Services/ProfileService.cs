using Ardalis.GuardClauses;
using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Post;
using DataAccessLayer.Entity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Profile = DataAccessLayer.Entity.Profile;

namespace BusinessLayer.Services
{
    public class ProfileService : GenericService<Profile>, IProfileService
    {
        private IMapper mapper;
        private PostService postService;
        private FileService fileService;

        public ProfileService(IRepository<Profile> repository, PostService postService, FileService fileService, IUnitOfWork uow, IMapper mapper) : base(repository, uow)
        {
            this.mapper = mapper;
            this.postService = postService;
            this.fileService = fileService;
        }

        public void addPost(int profileId, int userId, PostCreateDTO postDTO)
        {
            Post post = mapper.Map<Post>(postDTO);
            post.UserId = userId;
            post.PostableId = profileId;

            postService.Insert(post);
        }

        public void changeAvatar(int profileId, IFormFile avatar)
        {
            Guard.Against.Null(profileId);

            var file = fileService.saveFile(avatar);
            var profile = GetByID(profileId);
            profile.FileEntityId = file.Id;
            Update(profile);
        }
    }
}
