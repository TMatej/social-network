using DataAccessLayer.Entity;

namespace Infrastructure.Query
{
    public class QueryResult<TEntity> where TEntity : IEntity
    {
        public long TotalItemsCount { get; }
        public int? RequestedPageNumber { get; }
        public int? RequestedPageSize { get; }
        public IEnumerable<TEntity> Items { get; }

        public QueryResult(long totalItemsCount, int? requestedPageNumber, int? requestedPageSize, IEnumerable<TEntity> items)
        {
            TotalItemsCount = totalItemsCount;
            RequestedPageNumber = requestedPageNumber;
            RequestedPageSize = requestedPageSize;
            Items = items;
        }
    }
}
