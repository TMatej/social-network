using DataAccessLayer;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalUnitTests.EntityTests
{
    public class ContactTest
    {
        private User user1;
        private User user2;

        [SetUp]
        public void Setup()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                user1 = new User
                {
                    Username = "testUser1",
                    Email = "user1@gmail.com",
                    PasswordHash = "0123456789abcde0"
                };
                user2 = new User
                {
                    Username = "testUser2",
                    Email = "user2@gmail.com",
                    PasswordHash = "0123456789abcde0"
                };

                db.Users.Add(user1);
                db.Users.Add(user2);
                db.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Database.EnsureDeleted();
                db.Dispose();
            }
        }

        [Test]
        public void Add_Contact_Test()
        {
            //Arrange
            var contact = new Contact
            {
                User1Id = user1.Id,
                User2Id = user2.Id
            };
            
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                //Act
                db.Contacts.Add(contact);
                db.SaveChanges();
                //Assert
                var contact_ret = db.Contacts.FirstOrDefault();
                Assert.That(contact_ret, Is.Not.Null);
                Assert.That(contact_ret.Id, Is.EqualTo(1));
            }
        }

        [Test]
        public void Delete_Contact_Test()
        {
            //Arrange
            var contact = new Contact
            {
                User1Id = user1.Id,
                User2Id = user2.Id
            };

            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
            }

            //Act
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                var contact_ret = db.Contacts.FirstOrDefault();
                db.Contacts.Remove(contact_ret);
                db.SaveChanges();

                //Assert 
                contact_ret = db.Contacts.FirstOrDefault();
                Assert.That(contact_ret, Is.Null);
            }

        }

        [Test]
        public void Delete_User2_Of_Contact_Valid_Test()
        {
            //Arrange
            var contact = new Contact
            {
                User1Id = user1.Id,
                User2Id = user2.Id
            };

            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
            }

            //Act
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                var user = db.Users.OrderByDescending(u => u.Id).FirstOrDefault();
                db.Users.Remove(user);
                db.SaveChanges();

                //Assert 
                var contact_ret = db.Contacts.FirstOrDefault();
                var user1_ret = db.Users.FirstOrDefault();
                var user2_ret = db.Users.OrderByDescending(u => u.Id).FirstOrDefault();
                Assert.That(contact_ret, Is.Null);
                Assert.That(user1_ret, Is.Not.Null);
                Assert.That(user2_ret.Id, Is.EqualTo(user1_ret.Id));
            }
        }

        [Test]
        public void Delete_User1_Of_Contact_Throws_Test()
        {
            //Arrange
            var contact = new Contact
            {
                User1Id = user1.Id,
                User2Id = user2.Id
            };

            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
            }

            //Act
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                var user = db.Users.FirstOrDefault();
                db.Users.Remove(user);

                //Assert 
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());   
            }

        }
    }
}
