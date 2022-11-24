using AutoMapper;
using BusinessLayer.DTOs.Query.Filters;
using BusinessLayer.DTOs.Query.Results;
using DataAccessLayer.Entity;
using Infrastructure.Query;

namespace BusinessLayer.QueryObjects
{
    public class GenericQueryObject<TEntity>
        where TEntity : class, IEntity, new()

    {
        protected IMapper _mapper;
        protected IQuery<TEntity> _entityQuery;

        public GenericQueryObject(IMapper mapper, IQuery<TEntity> entityQuery)
        {
            _mapper = mapper;
            _entityQuery = entityQuery;
        }
        public GenericQueryObject<TEntity> ApplyWhereClause<TWhereType>(GenericWhereDTO<TWhereType> filter)
            where TWhereType : IComparable<TWhereType>
        {
            if (!string.IsNullOrEmpty(filter.WhereColumnName))
            {
                _entityQuery = _entityQuery.Where(filter.FilterWhereExpression, filter.WhereColumnName);
            }

            return this;
        }

        public GenericQueryObject<TEntity> ApplyOrderByClause<TOrderType>(GenericOrderByDTO<TOrderType> filter)
            where TOrderType : IComparable<TOrderType>
        {
            if (!string.IsNullOrEmpty(filter.OrderingColumnName))
            {
                _entityQuery = _entityQuery.OrderBy<TOrderType>(filter.OrderingColumnName, filter.IsAscending);
            }

            return this;
        }

        public QueryResultDto<TEntityDTO> ExecuteQuery<TEntityDTO>(GenericFilterDTO filter)
            where TEntityDTO : class, new()
        {
            var query = _entityQuery;

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

        public QueryResultDto<TEntityDTO> ExecuteQuery<TEntityDTO>()
            where TEntityDTO : class, new()
        {
            return ExecuteQuery<TEntityDTO>(new GenericFilterDTO());
        }
    }
}
