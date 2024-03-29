﻿using DataAccessLayer.Entity;

namespace BusinessLayer.Contracts
{
    public interface IPostService : IGenericService<Post>
    {
        public IEnumerable<Post> getPostsForEntity(int entityId, int page = 1, int pageSize = 10);
    }
}
