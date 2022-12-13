using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Post;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using System.Linq.Expressions;

namespace ServiceTests
{
    class PostServiceTest
    {
        IMapper mapper;
        IRepository<DataAccessLayer.Entity.Profile> profileRepository;
        IRepository<Post> postRepository;
        IQuery<Post> postQuery;
        IQuery<DataAccessLayer.Entity.Profile> profileQuery;
        IPostService postService;
        IFileService fileService;
        IUnitOfWork uow;

        [SetUp]
        public void Setup()
        {
            mapper = Substitute.For<IMapper>();
            postService = Substitute.For<IPostService>();
            fileService = Substitute.For<IFileService>();
            profileRepository = Substitute.For<IRepository<DataAccessLayer.Entity.Profile>>();
            postRepository = Substitute.For<IRepository<Post>>();
            postQuery = Substitute.For<IQuery<Post>>();
            profileQuery = Substitute.For<IQuery<DataAccessLayer.Entity.Profile>>();
            uow = Substitute.For<IUnitOfWork>();
        }

        [Test]
        public void AddPostTest()
        {
            var postDTO = new PostCreateDTO
            {
                Title = "Test",
            };
            var post = new Post
            {
                Id = 1,
                Title = "Test",
            };
            mapper.Map<Post>(postDTO).Returns(post);
            var profileService = new ProfileService(profileQuery, profileRepository, postService, fileService, uow, mapper);
            profileService.addPost(1, 1, postDTO);
            postService.Received(1).Insert(post);
        }

        [Test]
        public void GetPostsForEntityTest()
        {
            var post = new Post()
            {
                Id = 1,
                Content = "Test",
            };

            postQuery.Where<int>(Arg.Any<Expression<Func<int, bool>>>(), Arg.Any<string>()).Returns(postQuery);
            postQuery.Page(Arg.Any<int>(), Arg.Any<int>()).Returns(postQuery);
            postQuery.OrderBy<DateTime>(Arg.Any<string>()).Returns(postQuery);
            postQuery.Execute().Returns(new QueryResult<Post>(1, 3, 20, new List<Post>() { post }));

            var postService = new PostService(postRepository, uow, postQuery);
            var res = postService.getPostsForEntity(44, 3, 20);

            postQuery.Received(1).Where<int>(Arg.Any<Expression<Func<int, bool>>>(), "PostableId");
            postQuery.Received(1).Page(3, 20);
            postQuery.Received(1).OrderBy<DateTime>("CreatedAt");
            postQuery.Received(1).Execute();

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Count(), Is.EqualTo(1));
            Assert.That(res.First(), Is.EqualTo(post));
        }
    }
}
