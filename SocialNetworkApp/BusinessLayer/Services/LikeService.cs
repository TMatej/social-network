using BusinessLayer.Contracts;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LikeService : GenericService<Like>, ILikeService
    {
        readonly IQuery<Like> query;
        public LikeService(IQuery<Like> query,IRepository<Like> repository, IUnitOfWork uow) : base(repository, uow)
        {
            this.query = query;
        }
        public int GetPostLikes(int postId)
        {
            return query.Where<int>(id => id == postId, nameof(Like.PostId)).Execute().Items.Count();
        }

        public void LikePost(int postId, int userId)
        {
            var likeExists = query.Where<int>(id => id == postId, nameof(Like.PostId)).Where<int>(id => id == userId, nameof(Like.UserId)).Execute().Items.Any();
            if (!likeExists)
            {
                    var like = new Like
                    {
                        PostId = postId,
                        UserId = userId
                    };
                    _repository.Insert(like);
                    _uow.Commit();   
            }
        }

        public void UnlikePost(int postId, int userId)
        {
            var like = query.Where<int>(id => id == postId, nameof(Like.PostId)).Where<int>(id => id == userId, nameof(Like.UserId)).Execute().Items.FirstOrDefault();
            if (like != null)
            {
                _repository.Delete(like);
                _uow.Commit();
            }
        }
    }
}
