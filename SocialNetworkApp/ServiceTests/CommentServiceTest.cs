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
            var post = new Comment()
            {
                Id = 1,
                Content = "Test",
            };

            commentQuery.Where<int>(Arg.Any<Expression<Func<int, bool>>>(), Arg.Any<string>()).Returns(commentQuery);
            commentQuery.Page(Arg.Any<int>(), Arg.Any<int>()).Returns(commentQuery);
            commentQuery.OrderBy<DateTime>(Arg.Any<string>()).Returns(commentQuery);
            commentQuery.Execute().Returns(new QueryResult<Comment>(1, 3, 20, new List<Comment>() { post }));

            var commentService = new CommentService(mapper, commentQuery, commentRepo, uow);
            var res = commentService.getCommentsForEntity(44, 3, 20);

            commentQuery.Received(1).Where<int>(Arg.Any<Expression<Func<int, bool>>>(), "CommentableId");
            commentQuery.Received(1).Page(3, 20);
            commentQuery.Received(1).OrderBy<DateTime>("CreatedAt");
            commentQuery.Received(1).Execute();

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Items.Count(), Is.EqualTo(1));
            Assert.That(res.Items.First(), Is.EqualTo(post));
        }
    }
}
