using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Post;
using DataAccessLayer.Entity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace ServiceTests
{
    class PostServiceTest
    {
        IMapper mapper;
        IRepository<DataAccessLayer.Entity.Profile> profileRepository;
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
            var profileService = new ProfileService(profileRepository, postService, fileService, uow, mapper);
            profileService.addPost(1, 1, postDTO);
            postService.Received(1).Insert(post);
        }
    }
}
