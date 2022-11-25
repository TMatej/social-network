using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace ServiceTests
{

    class GroupServiceTest
    {
        IRepository<GroupMember> groupMemberRepository;
        IRepository<Group> groupRepository;
        IUnitOfWork uow;
        Group mockGroup;

        [SetUp]
        public void Setup()
        {
            groupMemberRepository = Substitute.For<IRepository<GroupMember>>();
            groupRepository = Substitute.For<IRepository<Group>>();
            uow = Substitute.For<IUnitOfWork>();

            var mockGroupMembers = new List<GroupMember>
            {
                new GroupMember
                {
                    GroupId = 1,
                    UserId = 1,
                    GroupRoleId = 1
                }
            };

            mockGroup = new Group
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                GroupMembers = mockGroupMembers,
            };
            groupRepository.GetAll().Returns(new List<Group>
            {
                mockGroup
            });
            groupMemberRepository.GetAll().Returns(mockGroupMembers);
        }

        [Test]
        public void GetByUserTest()
        {
            var user = new User()
            {
                Id = 1
            };
            var groupService = new GroupService(groupRepository, groupMemberRepository, uow);
            var res = groupService.GetByUser(user);
            Assert.That(res, Has.Exactly(1).Items);
            Assert.That(res, Has.Exactly(1).EqualTo(mockGroup));
        }

        [Test]
        public void AddToGroupTest()
        {
            var user = new User()
            {
                Id = 2
            };
            var groupService = new GroupService(groupRepository, groupMemberRepository, uow);
            groupService.AddToGroup(user, mockGroup, new GroupRole());
            groupMemberRepository.Received().Insert(Arg.Is<GroupMember>(x => x.UserId == user.Id && x.GroupId == mockGroup.Id));
            uow.Received().Commit();
        }

        [Test]
        public void RemoveFromGroupTest()
        {
            var user = new User()
            {
                Id = 1
            };
            var groupService = new GroupService(groupRepository, groupMemberRepository, uow);
            groupService.RemoveFromGroup(user, mockGroup);
            groupMemberRepository.Received().Delete(Arg.Is<GroupMember>(x => x.UserId == user.Id && x.GroupId == mockGroup.Id));
            uow.Received().Commit();
        }
    }
}
