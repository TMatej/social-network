﻿using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Comment;
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
    public class CommentService : GenericService<Comment>, ICommentService
    {
        private readonly IMapper mapper;
        private readonly IQuery<Comment> commentQuery;

        public CommentService(IMapper mapper, IQuery<Comment> commentQuery, IRepository<Comment> repository, IUnitOfWork uow) : base(repository, uow)
        {
            this.mapper = mapper;
            this.commentQuery = commentQuery;
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

        public QueryResult<Comment> getCommentsForEntity(int entityId, int page = 1, int pageSize = 10)
        {
            return commentQuery
                .Where<int>(id => id == entityId, "CommentableId")
                .Page(page, pageSize)
                .OrderBy<DateTime>("CreatedAt")
                .Execute();
        }
    }
}
