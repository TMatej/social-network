using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace ServiceTests
{
    public class ContactServiceTests
    {
        Contact contact;
        IRepository<Contact> repo;
        IUnitOfWork uow;
        IQuery<Contact> query;
        [SetUp]
        public void Setup()
        {
            contact = new Contact()
            {
                User1Id = 1,
                User2Id = 2,
            };
            repo = Substitute.For<IRepository<Contact>>();
            uow = Substitute.For<IUnitOfWork>();
            query = Substitute.For<IQuery<Contact>>();
        }

        [Test]
        public void Insert()
        {
            var contactService = new ContactService(repo, query, uow);
            contactService.Insert(contact);

            repo.Received(1).Insert(contact);
            uow.Received(1).Commit();
        }
    }
}