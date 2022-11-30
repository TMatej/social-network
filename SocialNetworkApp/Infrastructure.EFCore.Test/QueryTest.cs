using DataAccessLayer;
using DataAccessLayer.Entity;
using Infrastructure.EFCore.Query;
using Infrastructure.EFCore.UnitOfWork;

namespace Infrastructure.EFCore.Test
{
    public class QueryTest
    {
        private SocialNetworkDBContext dbContext;
        private EFUnitOfWork unitOfWork;

        [SetUp]
        public void Setup()
        {
            dbContext = new SocialNetworkDBContext();
            unitOfWork = new EFUnitOfWork(dbContext);

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
                Username = "thomas",
                PrimaryEmail = "thomas@gmail.com",
                PasswordHash = "541dremnb4"
            });
            dbContext.Users.Add(new User
            {
                Username = "bob",
                PrimaryEmail = "bob@gmail.com",
                PasswordHash = "6sdf198ve2"
            });
            dbContext.Users.Add(new User
            {
                Username = "john",
                PrimaryEmail = "john@gmail.com",
                PasswordHash = "51df6545ecvd"
            });
            dbContext.Users.Add(new User
            {
                Username = "peter",
                PrimaryEmail = "peter@gmail.com",
                PasswordHash = "5e21e65ver"
            });
            dbContext.Users.Add(new User
            {
                Username = "bradley",
                PrimaryEmail = "bradley@gmail.com",
                PasswordHash = "21ef5evc7"
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
        public void UserWithUsernameThomasExists_QueryWhere_Test()
        {
            var query = new EntityFrameworkQuery<User>(dbContext, unitOfWork);
            var username = "thomas";

            query.Where<string>(u => u == username, "Username");
            var result = query.Execute();

            Assert.True(result.Items.Count() == 1);
            Assert.True(result.Items.First().Username == username);
        }

        [Test]
        public void UsersWithEmailThatStartsWithBExist_QueryWhere_Test()
        {
            var query = new EntityFrameworkQuery<User>(dbContext, unitOfWork);
            query.Where<string>(a => a.StartsWith("b"), "PrimaryEmail");
            var result = query.Execute();

            Assert.True(result.Items.Count() == 3);
        }

        [Test]
        public void UsersWithIdLessThan3_QueryWhere_Test()
        {
            var query = new EntityFrameworkQuery<User>(dbContext, unitOfWork);
            query.Where<int>(a => a < 3, "Id");
            var result = query.Execute();

            Assert.True(result.Items.Count() == 2);
        }

        [Test]
        public void UsersOrderedAscending_QueryOrderBy_Test()
        {
            var query = new EntityFrameworkQuery<User>(dbContext, unitOfWork);
            query.OrderBy<int>("Id", true);
            var result = query.Execute()
                .Items
                .Select(a => a.Id)
                .ToList();

            var expected = dbContext.Users
                .Select(a => a.Id)
                .OrderBy(a => a)
                .ToList();

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void UsersOrderedDescending_QueryOrderBy_Test()
        {
            var query = new EntityFrameworkQuery<User>(dbContext, unitOfWork);
            var result = query
                .OrderBy<int>("Id", false)
                .Execute()
                .Items
                .Select(a => a.Id)
                .ToList();

            var expected = dbContext.Users
                .Select(a => a.Id)
                .OrderByDescending(a => a)
                .ToList();

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void UsersPagination_QueryPagination_Test()
        {
            var query = new EntityFrameworkQuery<User>(dbContext, unitOfWork);

            var result = query
                .OrderBy<int>("Id")
                .Page(2, 2)
                .Execute()
                .Items
                .Select(a => a.Id)
                .ToList();

            var expected = dbContext.Users
                .OrderBy(a => a.Id)
                .Skip(2)
                .Take(2)
                .Select(a => a.Id)
                .ToList();

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void AdvancedUsersQueryTest()
        {
            var query = new EntityFrameworkQuery<User>(dbContext, unitOfWork);
            var result = query
                .Where<string>(u => u.StartsWith("b"), "PrimaryEmail")
                .OrderBy<int>("Id", false)
                .Page(1, 2)
                .Execute()
                .Items
                .ToList();

            var expected = dbContext.Users
                .Where(a => a.PrimaryEmail.StartsWith("b"))
                .OrderByDescending(a => a.Id)
                .Take(2)
                .ToList();

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void UnknownColumnNameThrowsTest()
        {
            var query = new EntityFrameworkQuery<User>(dbContext, unitOfWork);
            var result = query
                .Where<string>(u => u.StartsWith("b"), "SomeRandomColumnNameThatDoesntExist");

            Assert.Throws<ArgumentNullException>(() => query.Execute());
        }
    }
}
