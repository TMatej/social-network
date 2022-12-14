using BusinessLayer.DTOs.Comment;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Facades.Interfaces
{
    public interface ICommentFacade
    {
        IEnumerable<CommentRepresentDTO> GetCommentsForEntity(int entityId, int page = 1, int pageSize = 10);
        void AddComment(int entityId, int userId, CommentCreateDTO commentDTO);
        void RemoveComment(int id);

    }
}
