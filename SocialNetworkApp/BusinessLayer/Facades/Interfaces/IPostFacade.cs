using BusinessLayer.DTOs.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Facades.Interfaces
{
    public interface IPostFacade
    {
        void LikePost(int postId, int userId);
        void UnlikePost(int postId, int userId);
        int GetPostLikes(int postId);
        void CreatePost(PostCreateDTO postDTO);
        void DeletePost(int postId);
        IEnumerable<PostRepresentDTO> GetPostForPostable(int postableId, int page, int pageSize);
    }
}
