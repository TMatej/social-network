using DataAccessLayer;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
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
            dbContext = new SocialNetworkDBContext("social-network-test-db");
            unitOfWork = new EFUnitOfWork(dbContext);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            dbContext.Users.Add(new User
            {
                Username = "ben",
                Email = "ben@gmail.com",
                PasswordHash = "aaafht3x"
            });
            dbContext.Users.Add(new User
            {
                Username = "thomas",
                Email = "thomas@gmail.com",
                PasswordHash = "541dremnb4"
            });
            dbContext.Users.Add(new User
            {
                Username = "bob",
                Email = "bob@gmail.com",
                PasswordHash = "6sdf198ve2"
            });
            dbContext.Users.Add(new User
            {
                Username = "john",
                Email = "john@gmail.com",
                PasswordHash = "51df6545ecvd"
            });
            dbContext.Users.Add(new User
            {
                Username = "peter",
                Email = "peter@gmail.com",
                PasswordHash = "5e21e65ver"
            });
            dbContext.Users.Add(new User
            {
                Username = "bradley",
                Email = "bradley@gmail.com",
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
        public void Include_UserRoles_Test()
        {
            var adminRole = new Role
            {
                Name = "Admin"
            };
            dbContext.Roles.Add(adminRole);
            dbContext.SaveChanges();

            var userRole = new Role
            {
                Name = "User"
            };
            dbContext.Roles.Add(userRole);
            dbContext.SaveChanges();

            var adminAdmin = new UserRole
            {
                UserId = 1,
                RoleId = 1
            };

            var userUser = new UserRole
            {
                UserId = 2,
                RoleId = 2
            };
            dbContext.UserRoles.AddRange(adminAdmin, userUser);
            dbContext.SaveChanges();

            using (var dbContextIn = new SocialNetworkDBContext("social-network-test-db"))
            {
                var unitOfWorkIn = new EFUnitOfWork(dbContextIn);
                var query = new EntityFrameworkQuery<User>(dbContextIn, unitOfWorkIn);
                var email = "ben@gmail.com";

                var user = query.Where<string>(u => u == email, nameof(User.Email))
                  .Include(nameof(User.UserRoles))
                  .Include(nameof(User.UserRoles) + "." + nameof(UserRole.Role))
                  .Include(nameof(User.Avatar))
                  .Execute()
                  .Items
                  .FirstOrDefault();

                Assert.That(user, Is.Not.Null);
                Assert.That(user.UserRoles, Is.Not.Null);
                Assert.That(user.UserRoles, Is.Not.Empty);
                Assert.That(user.UserRoles.FirstOrDefault(), Is.Not.Null);
                Assert.That(user.UserRoles.First().RoleId, Is.EqualTo(1));
                Assert.That(user.UserRoles.First().Role, Is.Not.Null);
                Assert.That(user.UserRoles.First().Role.Name, Is.EqualTo("Admin"));
            }
            
        }

        [Test]
        public void UserWithUsernameThomasExists_QueryWhere_Test()
        {
            var query = new EntityFrameworkQuery<User>(dbContext, unitOfWork);
            var username = "thomas";

            query.Where<string>(u => u == username, nameof(User.Username));
            var result = query.Execute();

            Assert.True(result.Items.Count() == 1);
            Assert.True(result.Items.First().Username == username);
        }

        [Test]
        public void UsersWithEmailThatStartsWithBExist_QueryWhere_Test()
        {
            var query = new EntityFrameworkQuery<User>(dbContext, unitOfWork);
            query.Where<string>(a => a.StartsWith("b"), "Email");
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
                .Where<string>(u => u.StartsWith("b"), "Email")
                .OrderBy<int>("Id", false)
                .Page(1, 2)
                .Execute()
                .Items
                .ToList();

            var expected = dbContext.Users
                .Where(a => a.Email.StartsWith("b"))
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
