﻿using Ardalis.GuardClauses;
using AutoMapper;
using BusinessLayer.DTOs.Post;
using DataAccessLayer.Entity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Profile = DataAccessLayer.Entity.Profile;

namespace BusinessLayer.Services
{
    internal class ProfileService : GenericService<Profile>
    {
        private IMapper mapper;
        private PostService postService;
        private ProfileService profileService;
        private FileService fileService;

        public ProfileService(IRepository<Profile> repository, PostService postService, FileService fileService, ProfileService profileService, IUnitOfWork uow, IMapper mapper) : base(repository, uow)
        {
            this.mapper = mapper;
            this.postService = postService;
            this.fileService = fileService;
            this.profileService = profileService;
        }

        public void addPost(Profile profile, int userId, PostCreateDTO postDTO)
        {
            Post post = mapper.Map<Post>(postDTO);
            post.UserId = userId;
            post.PostableId = profile.Id;

            postService.Insert(post);
        }

        public void changeAvatar(int profileId, IFormFile avatar)
        {
            Guard.Against.Null(profileId);

            var file = fileService.saveFile(avatar);
            var profile = GetByID(profileId);
            profile.FileId = file.Id;
            Update(profile);
        }
    }
}
