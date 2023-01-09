using AutoMapper;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using System.Linq.Expressions;

namespace ServiceTests
{
    class CommentServiceTest
    {
        IRepository<Comment> commentRepo;
        IQuery<Comment> commentQuery;
        IUnitOfWork uow;
        IMapper mapper;

        [SetUp]
        public void Setup()
        {
            commentRepo = Substitute.For<IRepository<Comment>>();
            commentQuery = Substitute.For<IQuery<Comment>>();
            uow = Substitute.For<IUnitOfWork>();
            mapper = Substitute.For<IMapper>();
        }

        [Test]
        public void GetCommentsForEntityTest()
        {
            var comment = new Comment()
            {
                Id = 1,
                Content = "Test",
            };

            var commentQuery = MockQuery.CreateMockQueryWithResult<Comment>(new QueryResult<Comment>(1, 3, 20, new List<Comment>() { comment }));
            var commentService = new CommentService(mapper, commentQuery, commentRepo, uow);
            var res = commentService.getCommentsForEntity(44, 3, 20);

            commentQuery.Received(1).Where<int>(Arg.Any<Expression<Func<int, bool>>>(), nameof(Comment.CommentableId));
            commentQuery.Received(1).Page(3, 20);
            commentQuery.Received(1).OrderBy<DateTime>(nameof(Comment.CreatedAt));
            commentQuery.Received(1).Execute();

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Count(), Is.EqualTo(1));
            Assert.That(res.First(), Is.EqualTo(comment));
        }
    }
}
