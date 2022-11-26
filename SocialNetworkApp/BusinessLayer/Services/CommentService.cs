using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Comment;
using DataAccessLayer.Entity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CommentService : GenericService<Comment>, ICommentService
    {
        private IMapper mapper;

        public CommentService(IMapper mapper, IRepository<Comment> repository, IUnitOfWork uow) : base(repository, uow)
        {
            this.mapper = mapper;
        }

        public void AddComment(CommentCreateDTO commentDTO)
        {
            Insert(mapper.Map<Comment>(commentDTO));
        }

        public void EditComment(CommentEditDTO commentDTO)
        {
            Update(mapper.Map<Comment>(commentDTO));
        }

        public CommentRepresentDTO GetDetailedCommentById(int id)
        {
            return mapper.Map<CommentRepresentDTO>(GetByID(id));
        }

        public CommentBasicRepresentDTO GetPlainCommentById(int id)
        {
            return mapper.Map<CommentBasicRepresentDTO>(GetByID(id));
        }
    }
}
