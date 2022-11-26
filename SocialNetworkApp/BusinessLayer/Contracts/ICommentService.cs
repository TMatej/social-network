using BusinessLayer.DTOs.Comment;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface ICommentService : IGenericService<Comment>
    {
        void AddComment(CommentCreateDTO commentDTO);
        void EditComment(CommentEditDTO commentDTO);
        CommentRepresentDTO GetDetailedCommentById(int id);
        CommentBasicRepresentDTO GetPlainCommentById(int id);
    }
}
