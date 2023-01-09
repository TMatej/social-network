using DataAccessLayer.Entity;
using Infrastructure.Query;
using NSubstitute.Extensions;

namespace ServiceTests
{
    internal static class MockQuery
    {
        public static IQuery<T> CreateMockQuery<T>() where T : class, IEntity, new()
        {
            var mQuery = Substitute.For<IQuery<T>>();
            mQuery.Execute().Returns(new QueryResult<T>(1, 3, 20, new List<T>()));
            mQuery.ReturnsForAll(mQuery);
            return mQuery;
        }
        public static IQuery<T> CreateMockQueryWithResult<T>(QueryResult<T> res) where T : class, IEntity, new()
        {
            var mQuery = Substitute.For<IQuery<T>>();
            mQuery.Execute().Returns(res);
            mQuery.ReturnsForAll(mQuery);
            return mQuery;
        }
    }
}
