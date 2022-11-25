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
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=PV179-SocialNetworkDB";

        [SetUp]
        public void Setup()
        {
            dbContext = new SocialNetworkDBContext(ConnectionString);
            unitOfWork = new EFUnitOfWork(dbContext);
            repository = new EFGenericRepository<User>(unitOfWork);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            dbContext.Users.Add(new User
            {
                Username = "ben",
                PrimaryEmail = "ben@gmail.com",
                PasswordHash = "aaafht3x"
            });

            dbContext.Users.Add(new User
            {
                Username = "john",
                PrimaryEmail = "john@gmail.com",
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
                Username = "thomas",
                PrimaryEmail = "thomas@gmail.com",
                PasswordHash = "541dremnb4"
            });

            dbContext.SaveChanges();

            var countAfter = dbContext.Users.Count();
            Assert.IsTrue(countAfter == countBefore + 1);

            var user = dbContext.Users.Where(u => u.Username == "thomas");
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
            var user = dbContext.Users.Where(u => u.Username == "ben").First();
            var found = repository.GetByID(user.Id);

            Assert.That(found, Is.EqualTo(user));
        }

        [Test]
        public void DeleteTest()
        {
            var user = dbContext.Users.Where(u => u.Username == "ben").First();
            repository.Delete(user.Id);
            dbContext.SaveChanges();

            var count = dbContext.Users.Where(u => u.Username == "ben").Count();
            Assert.True(count == 0);
        }

        [Test]
        public void UpdateTest()
        {
            var user = dbContext.Users.Where(u => u.Username == "ben").First();

            user.Username = "theNewBen";
            repository.Update(user);
            dbContext.SaveChanges();

            var count = dbContext.Users.Where(u => u.Username == "theNewBen").Count();
            Assert.That(count == 1);
        }
    }
}