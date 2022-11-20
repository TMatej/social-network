using BusinessLayer.DTOs.Gallery.Results;
using BusinessLayer.DTOs.Gallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using BusinessLayer.DTOs.Gallery.Filters;

namespace BusinessLayer.QueryObjects
{
    public abstract class GenericQueryObject<TEntity> 
        where TEntity : class, IEntity, new()
        
    {
        protected IMapper _mapper;
        protected IQuery<TEntity> _entityQuery;

        public GenericQueryObject(IMapper mapper, IQuery<TEntity> entityQuery)
        {
            _mapper = mapper;
            _entityQuery = entityQuery;
        }
        public QueryResultDto<TEntityDTO> ExecuteQuery<TColumnType, TEntityDTO>(GenericFilterDTO filter) 
            where TColumnType : IComparable<TColumnType>
            where TEntityDTO : class, new()
        {
            var query = ApplyWhere(filter);

            if (!string.IsNullOrEmpty(filter.OrderingColumnName))
            {
                query = query.OrderBy<TColumnType>(filter.OrderingColumnName, filter.IsAscending);
            }

            if (filter.RequestedPageNumber.HasValue)    // create paging only if the requested page was specified
            {
                query = filter.RequestedPageSize.HasValue ? query.Page(filter.RequestedPageNumber.Value, filter.RequestedPageSize.Value) : query.Page(filter.RequestedPageNumber.Value);
            }

            if (filter.IncludeParameters.Any())
            {
                foreach (var parameter in filter.IncludeParameters)
                {
                    query = query.Include(parameter);
                }
            }

            return _mapper.Map<QueryResultDto<TEntityDTO>>(query.Execute());
        }

        protected abstract IQuery<TEntity> ApplyWhere(GenericFilterDTO filter);
    }
}
