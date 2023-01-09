using DataAccessLayer.Entity;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity.JoinEntity;

namespace DalUnitTests.EntityTests
{
    public class EventParticipantTest
    {
        [SetUp]
        public void Setup()
        {
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Users.Add(new User
                {
                    Username = "ben",
                    Email = "ben@gmail.com",
                    PasswordHash = "aaafht3x"
                });
                db.SaveChanges();

                db.Groups.Add(new Group
                {
                    Name = "Test Group",
                    Description = "This is a test group",
                });
                db.SaveChanges();

                db.Events.Add(new Event
                {
                    UserId = 1,
                    GroupId = 1,
                    Title = "Test Event",
                    Description = "This is a test event",
                });
                db.SaveChanges();

                db.ParticipationTypes.Add(new ParticipationType
                {
                    Name = "Visitor"
                });
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
        public void Add_Test()
        {
            // Arrange
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.EventParticipants.Add(new EventParticipant
                {
                    UserId = 1,
                    EventId = 1,
                    ParticipationTypeId = 1
                });
                db.SaveChanges();
            }

            // Act
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                var eventParticipant = db.EventParticipants.FirstOrDefault();
                // Assert
                Assert.That(eventParticipant, Is.Not.Null);
                Assert.That(eventParticipant.Id, Is.EqualTo(1));
            }  
        }

        [Test]
        public void Delete_Test()
        {
            // Arrange
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                db.EventParticipants.Add(new EventParticipant
                {
                    UserId = 1,
                    EventId = 1,
                    ParticipationTypeId = 1
                });
                db.SaveChanges();
            }

            // Act
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                var eventParticipant = db.EventParticipants.First();
                db.EventParticipants.Remove(eventParticipant);
                db.SaveChanges();
            }

            // Assert
            using (var db = new SocialNetworkDBContext("social-network-test-db"))
            {
                var eventParticipant = db.EventParticipants.FirstOrDefault();
                var user = db.Users.FirstOrDefault();
                var participationType = db.ParticipationTypes.FirstOrDefault();
                var event_ret = db.Events.FirstOrDefault();
                Assert.That(eventParticipant, Is.Null);
                Assert.That(user, Is.Not.Null);
                Assert.That(participationType, Is.Not.Null);
                Assert.That(event_ret, Is.Not.Null);
            }
        }
    }
}
