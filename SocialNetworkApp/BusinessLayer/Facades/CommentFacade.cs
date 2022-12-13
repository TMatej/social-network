using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Comment;
using BusinessLayer.DTOs.Event;
using BusinessLayer.Facades.Interfaces;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Facades
{
    public class CommentFacade : ICommentFacade
    {
        ICommentService commentService;
        IMapper mapper;

        public CommentFacade(ICommentService commentService, IMapper mapper)
        {
            this.commentService = commentService;
            this.mapper = mapper;
        }

        public void AddComment(CommentCreateDTO commentDTO)
        {
            commentService.AddComment(commentDTO);
        }

        public IEnumerable<CommentRepresentDTO> GetCommentsForEntity(int entityId, int page = 1, int pageSize = 10)
        {
            var services = commentService.getCommentsForEntity(entityId, page, pageSize);
            return services.Select(e => mapper.Map<CommentRepresentDTO>(e));
        }

        public void RemoveComment(int id)
        {
            commentService.Delete(id);
        }
    }
}
