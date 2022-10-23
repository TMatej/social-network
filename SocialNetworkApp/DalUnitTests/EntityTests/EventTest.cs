﻿using DataAccessLayer.Entity;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalUnitTests.EntityTests
{
    public class EventTest
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=PV179-SocialNetworkDB";

        [SetUp]
        public void Setup()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Database.EnsureDeleted();
                db.Dispose();
            }
        }
        [Test]
        public void Test_Add()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Events.Add(new Event
                {
                    UserId = 1,
                    GroupId = 2,
                    Title = "Example Event",
                    Description = "This is an example event",
                    CreatedAt = DateTime.Now
                });
                db.SaveChanges();

                var _event = db.Events.FirstOrDefault();
                Assert.That(_event, Is.Not.Null);
                Assert.That(_event.Id, Is.EqualTo(1));
            }
        }

        [Test]
        public void Test_Add_Incomplete()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Events.Add(new Event
                {
                    Title = "Example Event",
                    Description = "This is an example event",
                    CreatedAt = DateTime.Now
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
        [Test]
        public void Test_Add_Long()
        {
            using (var db = new SocialNetworkDBContext(connectionString))
            {
                db.Events.Add(new Event
                {
                    UserId = 1,
                    GroupId = 2,
                    Title = new String('l',1000),
                    Description = new String('l', 1000),
                    CreatedAt = DateTime.Now
                });
                Assert.Throws<DbUpdateException>(() => db.SaveChanges());
            }
        }
    }
}