using DataAccessLayer;
using DataAccessLayer.Entity;
using Infrastructure.EFCore.Query;

namespace Infrastructure.EFCore.Test
{
    public class Tests
    {
        private SocialNetworkDBContext dbContext;
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=PV179-SocialNetworkDB";

        [SetUp]
        public void Setup()
        {
            dbContext = new SocialNetworkDBContext(ConnectionString);

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
            var query = new EntityFrameworkQuery<User>(dbContext);
            var username = "thomas";

            query.Where<string>(u => u == username, "Username");
            var result = query.Execute();

            Assert.True(result.Count() == 1);
            Assert.True(result.First().Username == username);
        }

        [Test]
        public void UsersWithEmailThatStartsWithBExist_QueryWhere_Test()
        {
            var query = new EntityFrameworkQuery<User>(dbContext);
            query.Where<string>(a => a.StartsWith("b"), "PrimaryEmail");
            var result = query.Execute();

            Assert.True(result.Count() == 3);
        }

        [Test]
        public void UsersWithIdLessThan3_QueryWhere_Test()
        {
            var query = new EntityFrameworkQuery<User>(dbContext);
            query.Where<int>(a => a < 3, "Id");
            var result = query.Execute();

            Assert.True(result.Count() == 2);
        }

        [Test]
        public void UsersOrderedAscending_QueryOrderBy_Test()
        {
            var query = new EntityFrameworkQuery<User>(dbContext);
            query.OrderBy<int>("Id", true);
            var result = query.Execute()
                .Select(a => a.Id)
                .ToList();

            var ExpectedResult = dbContext.Users
                .Select(a => a.Id)
                .OrderBy(a => a)
                .ToList();

            Assert.That(result, Is.EqualTo(ExpectedResult));
        }

        [Test]
        public void UsersOrderedDescending_QueryOrderBy_Test()
        {
            var query = new EntityFrameworkQuery<User>(dbContext);
            query.OrderBy<int>("Id", false);
            var result = query.Execute()
                .Select(a => a.Id)
                .ToList();

            var ExpectedResult = dbContext.Users
                .Select(a => a.Id)
                .OrderByDescending(a => a)
                .ToList();

            Assert.That(result, Is.EqualTo(ExpectedResult));
        }

        [Test]
        public void UsersPagination_QueryPagination_Test()
        {
            var query = new EntityFrameworkQuery<User>(dbContext);

            var result = query
                .OrderBy<int>("Id")
                .Page(2, 2)
                .Execute()
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
            var query = new EntityFrameworkQuery<User>(dbContext);
            var result = query
                .Where<string>(u => u.StartsWith("b"), "PrimaryEmail")
                .OrderBy<int>("Id", false)
                .Page(1, 2)
                .Execute()
                .ToList();

            var expected = dbContext.Users
                .Where(a => a.PrimaryEmail.StartsWith("b"))
                .OrderByDescending(a => a.Id)
                .Take(2)
                .ToList();

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}