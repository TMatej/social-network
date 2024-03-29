﻿using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Post;
using BusinessLayer.Facades.Interfaces;
using DataAccessLayer.Entity;

namespace BusinessLayer.Facades
{
    public class PostFacade : IPostFacade
    {
        IPostService postService;
        ILikeService likeService;
        IUserService userService;
        IMapper mapper;

        public PostFacade(IPostService postService, ILikeService likeService, IUserService userService , IMapper mapper)
        {
            this.postService = postService;
            this.likeService = likeService;
            this.userService = userService;
            this.mapper = mapper;
            
        }
        public void CreatePost(PostCreateDTO postDTO)
        {
            var post = new Post()
            {
                UserId = postDTO.UserId,
                PostableId = postDTO.PostableId,
                Title = postDTO.Title,
                Content = postDTO.Content
            };
            postService.Insert(post);
        }

        public void DeletePost(int postId)
        {
            postService.Delete(postId);
        }

        public IEnumerable<PostRepresentDTO> GetPostForPostable(int postableId, int page, int pageSize)
        {
            var posts = postService.getPostsForEntity(postableId,page,pageSize);
            return posts.Select(post => mapper.Map<PostRepresentDTO>(post));
        }

        public int GetPostLikes(int postId)
        {
            return likeService.GetPostLikes(postId);
        }

        public void LikePost(int postId, int userId)
        {
            likeService.LikePost(postId, userId);
        }

        public void UnlikePost(int postId, int userId)
        {
            likeService.UnlikePost(postId, userId);
        }
        public bool CheckPermission(string claimId, int postId)
        {
            var post = postService.GetByID(postId);
            return post.UserId == int.Parse(claimId) || userService.IsAdmin(int.Parse(claimId));
        }
    }
}
