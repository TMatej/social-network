using DataAccessLayer;
using DataAccessLayer.Entity;
using Infrastructure.EFCore.Repository;
using Infrastructure.EFCore.UnitOfWork;

namespace Infrastructure.EFCore.Test
{
    public class RepositoryTest
    {
        private SocialNetworkDBContext dbContext;
        private EFUnitOfWork unitOfWork;
        private EFGenericRepository<User> repository;

        [SetUp]
        public void Setup()
        {
            dbContext = new SocialNetworkDBContext("social-network-test-db");
            unitOfWork = new EFUnitOfWork(dbContext);
            repository = new EFGenericRepository<User>(unitOfWork);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            dbContext.Users.Add(new User
            {
                Name = "ben",
                Email = "ben@gmail.com",
                PasswordHash = "aaafht3x"
            });

            dbContext.Users.Add(new User
            {
                Name = "john",
                Email = "john@gmail.com",
                PasswordHash = "51df6545ecvd"
            });

            dbContext.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Test]
        public void InsertTest()
        {
            var countBefore = dbContext.Users.Count();

            repository.Insert(new User
            {
                Name = "thomas",
                Email = "thomas@gmail.com",
                PasswordHash = "541dremnb4"
            });

            dbContext.SaveChanges();

            var countAfter = dbContext.Users.Count();
            Assert.IsTrue(countAfter == countBefore + 1);

            var user = dbContext.Users.Where(u => u.Name == "thomas");
            Assert.IsNotNull(user);
        }

        [Test]
        public void GetAllTest()
        {
            var found = repository.GetAll();
            var expected = dbContext.Users.ToList();

            Assert.That(found, Is.EqualTo(expected));
        }

        [Test]
        public void GetByIdTest()
        {
            var user = dbContext.Users.Where(u => u.Name == "ben").First();
            var found = repository.GetByID(user.Id);

            Assert.That(found, Is.EqualTo(user));
        }

        [Test]
        public void DeleteTest()
        {
            var user = dbContext.Users.Where(u => u.Name == "ben").First();
            repository.Delete(user.Id);
            dbContext.SaveChanges();

            var count = dbContext.Users.Where(u => u.Name == "ben").Count();
            Assert.True(count == 0);
        }

        [Test]
        public void UpdateTest()
        {
            var user = dbContext.Users.Where(u => u.Name == "ben").First();

            user.Name = "theNewBen";
            repository.Update(user);
            dbContext.SaveChanges();

            var count = dbContext.Users.Where(u => u.Name == "theNewBen").Count();
            Assert.That(count == 1);
        }
    }
}
