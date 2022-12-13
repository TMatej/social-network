using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface ILikeService
    {
        void LikePost(int postId, int userId);
        void UnlikePost(int postId, int userId);
        int GetPostLikes(int postId);
    }
}
