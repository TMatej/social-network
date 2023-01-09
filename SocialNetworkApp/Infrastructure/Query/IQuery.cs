using DataAccessLayer.Entity;
using System.Linq.Expressions;

namespace Infrastructure.Query
{
    public interface IQuery<TEntity> where TEntity : class, IEntity, new()
    {
        // IQuery<TEntity> DeleteFrom<T>(Expression<Func<T, bool>> rootPredicate, string columnName) where T : IComparable<T>;
        /// <summary>
        /// Metod <c>Where</c> is used as a Where clause in DB world.
        /// </summary>
        /// <typeparam name="T">Type of property in columnName.</typeparam>
        /// <param name="rootPredicate"></param>
        /// <param name="columnName"></param>
        /// <returns>The class object itself as it utilizes a builder pattern.
        /// To execute Query, run method Execute().</returns>
        IQuery<TEntity> Where<T>(Expression<Func<T, bool>> rootPredicate, string columnName) where T : IComparable<T>;

        IQuery<TEntity> OrderBy<T>(string columnName, bool ascendingOrder = true) where T : IComparable<T>;

        IQuery<TEntity> Page(int pageToFetch, int pageSize = 10);

        IQuery<TEntity> Include(string columnname);

        IQuery<TEntity> Include(params string[] parameters);

        QueryResult<TEntity> Execute();
    }
}
