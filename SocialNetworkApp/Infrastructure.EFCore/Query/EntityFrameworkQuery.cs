using DataAccessLayer;
using DataAccessLayer.Entity;
using Infrastructure.EFCore.ExpressionHelpers;
using Infrastructure.EFCore.UnitOfWork;
using Infrastructure.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.EFCore.Query
{
    public class EntityFrameworkQuery<TEntity> : Query<TEntity> where TEntity : class, IEntity, new()
    {
        protected SocialNetworkDBContext Dbcontext { get; set; }
        protected EFUnitOfWork UnitOfWork { get; set; }

        public EntityFrameworkQuery(SocialNetworkDBContext dbcontext, EFUnitOfWork unitOfWork)
        {
            Dbcontext = dbcontext;
            UnitOfWork = unitOfWork;
        }

        public override QueryResult<TEntity> Execute()
        {
            IQueryable<TEntity> query = Dbcontext.Set<TEntity>();

            if (WherePredicate.Count != 0)
            {
                query = ApplyWhere(query);
            }

            if (IncludeParameters.Count != 0)
            {
                query.ExecuteDelete();
            }

            if (IncludeParameters.Count != 0)
            {
                query = ApplyInclude(query);
            }

            if (OrderByContainer != null)
            {
                query = OrderBy(query);
            }

            if (PaginationContainer.HasValue)
            {
                query = Pagination(query);
            }
            var result = query.ToList();

            return new QueryResult<TEntity>(result.Count, PaginationContainer?.PageToFetch, PaginationContainer?.PageSize, result);
        }

        private IQueryable<TEntity> ApplyInclude(IQueryable<TEntity> query)
        {
            foreach (var include in IncludeParameters)
            {
                query = query.Include(include);
            }

            return query;
        }

        private IQueryable<TEntity> ApplyWhere(IQueryable<TEntity> query)
        {
            foreach (var expr in WherePredicate)
            {
                // parameter for new lambda
                var p = Expression.Parameter(typeof(TEntity), "p");

                // get the property name from the etity. For instance, its the same as calling `nameof(Subject.Name)`
                var columnNameFromObject = typeof(TEntity)
                    .GetProperty(expr.columnName)
                    ?.Name;

                // basically creates the property call, i.e -> p.Name
                var exprProp = Expression.Property(p, columnNameFromObject);// nameof(Customer.CustomerID));
                                                                            // replace parameter in original expression
                var expression = expr.expression;

                // gets the Expression Parameters
                var parameters = (IReadOnlyCollection<ParameterExpression>)expression
                    .GetType()
                    .GetProperty("Parameters")
                    ?.GetValue(expression);

                // gets the expression body
                var body = (Expression)expression
                    .GetType()
                    .GetProperty("Body")
                    ?.GetValue(expression);

                // (both) replaces the old lambda parameter with the new one
                // Example:
                //      a => a > 10
                // ->   p => p.Price > 10
                var visitor = new ReplaceParamVisitor(parameters.First(), exprProp);
                var exprNewBody = visitor.Visit(body);

                // creates the new lambda expression
                var lambda = Expression.Lambda<Func<TEntity, bool>>(exprNewBody, p);

                query = query.Where(lambda);
            }

            return query;
        }

        private IQueryable<TEntity> OrderBy(IQueryable<TEntity> query)
        {
            var orderByColumn = OrderByContainer.Value.columnName;
            var isAscending = OrderByContainer.Value.isAscending;
            var argumentType = OrderByContainer.Value.argumentType;

            var p = Expression.Parameter(typeof(TEntity), "p");

            var columnNameFromObject = typeof(TEntity)
                .GetProperty(orderByColumn)
                ?.Name;

            var exprProp = Expression.Property(p, columnNameFromObject);
            var lambda = Expression.Lambda(exprProp, p);

            var orderByMethod = typeof(Queryable)
                .GetMethods()
                .First(a => a.Name == (isAscending ? "OrderBy" : "OrderByDescending") && a.GetParameters().Length == 2);

            var orderByClosedMethod = orderByMethod.MakeGenericMethod(typeof(TEntity), argumentType);

            return (IQueryable<TEntity>)orderByClosedMethod.Invoke(null, new object[] { query, lambda })!;
        }

        private IQueryable<TEntity> Pagination(IQueryable<TEntity> query)
        {
            var page = PaginationContainer.Value.PageToFetch;
            var pageSize = PaginationContainer.Value.PageSize;

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
