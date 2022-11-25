using DataAccessLayer.Entity;
using System.Linq.Expressions;

namespace Infrastructure.Query
{
    public abstract class Query<TEntity> : IQuery<TEntity> where TEntity : class, IEntity, new()
    {
        public List<(Expression expression, Type argumentType, string columnName)> WherePredicate { get; set; } = new();
        public (string columnName, bool isAscending, Type argumentType)? OrderByContainer { get; set; }
        public (int PageToFetch, int PageSize)? PaginationContainer { get; set; }
        public List<string> IncludeParameters { get; set; } = new();

        public IQuery<TEntity> Page(int pageToFetch, int pageSize = 10)
        {
            PaginationContainer = (pageToFetch, pageSize);
            return this;
        }

        public IQuery<TEntity> OrderBy<T>(string columnName, bool ascendingOrder = true) where T : IComparable<T>
        {
            OrderByContainer = (columnName, ascendingOrder, typeof(T));
            return this;
        }

        public IQuery<TEntity> Where<T>(Expression<Func<T, bool>> predicate, string columnName) where T : IComparable<T>
        {
            WherePredicate.Add((predicate, typeof(T), columnName));
            return this;
        }

        public IQuery<TEntity> Include(string parameter)
        {
            IncludeParameters.Add(parameter);
            return this;
        }

        public IQuery<TEntity> Include(List<string> parameters)
        {
            parameters.ForEach(p => IncludeParameters.Add(p));
            return this;
        }

        public abstract QueryResult<TEntity> Execute();
    }
}
