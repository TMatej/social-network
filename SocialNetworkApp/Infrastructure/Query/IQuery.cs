using DataAccessLayer.Entity;
using System.Linq.Expressions;

namespace Infrastructure.Query
{
    public interface IQuery<TEntity> where TEntity : class, IEntity, new()
    {
        IQuery<TEntity> Where<T>(Expression<Func<T, bool>> rootPredicate, string columnName) where T : IComparable<T>;

        IQuery<TEntity> OrderBy<T>(string columnName, bool ascendingOrder = true) where T : IComparable<T>;

        IQuery<TEntity> Page(int pageToFetch, int pageSize = 10);

        IEnumerable<TEntity> Execute();
    }
}
