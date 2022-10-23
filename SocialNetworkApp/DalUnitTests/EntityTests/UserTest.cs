using DataAccessLayer;
using DataAccessLayer.Entity;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalUnitTests.EntityTests
{
    public class UserTest
    {
        [Test]
        public void Test1()
        {
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            if (connectionString == null)
            {
                Assert.Fail("Null connection string");
            }

            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Database.EnsureCreated();

                db.Users.Add(new User { Username = "lokomotiva123", PrimaryEmail = "cokoloko@gmail.com", PasswordHash = "0123456789abcde0" });
                db.SaveChanges();

                var user = db.Users.FirstOrDefault();
                Assert.NotNull(user);
                Assert.Equals(user.Id, 1);
            }
            
        }
    }
}
