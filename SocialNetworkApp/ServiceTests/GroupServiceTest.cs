using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Query;
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
            var mockGroupMember = new GroupMember
            {
                GroupId = 1,
                UserId = 1,
                GroupRoleId = 1
            };
            var mockGroupMembers = new List<GroupMember>
            {
                mockGroupMember
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
        public void RemoveFromGroupTest()
        {

            var user = new User()
            {
                Id = 1
            };
            var result = new QueryResult<GroupMember>(1, 1, 1, new List<GroupMember> { mockGroup.GroupMembers.First() });
            var queryWithResult = MockQuery.CreateMockQueryWithResult(result);
            var groupService = new GroupService(queryWithResult, groupRepository, groupMemberRepository, uow);
            groupService.RemoveFromGroup(user, mockGroup);
            groupMemberRepository.Received().Delete(Arg.Is<GroupMember>(x => x.UserId == user.Id && x.GroupId == mockGroup.Id));
            uow.Received().Commit();
        }
    }
}
