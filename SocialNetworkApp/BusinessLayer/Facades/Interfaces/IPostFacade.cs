using BusinessLayer.DTOs.Post;

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
