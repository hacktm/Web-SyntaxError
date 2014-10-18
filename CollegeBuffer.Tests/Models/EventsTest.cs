using System;
using System.Data.Entity.Migrations;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.DAL.Model.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollegeBuffer.Tests.Models
{
    [TestClass]
    public class EventsTest
    {
        [TestMethod]
        public void TestEventsCreation()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "user1",
                Password = "pass",
                EMail = "user1@user.com",
                Role = UserRoles.Student,
            };

            var subject1 = new Subject
            {
                Id = Guid.NewGuid(),
                Name = "subj"
            };

            var subject2 = new Subject
            {
                Id = Guid.NewGuid(),
                Name = "subj"
            };

            using (var db = new DatabaseContext())
            {
                user = db.Users.Add(user);

                Assert.AreNotEqual(db.SaveChanges(), 0);

                subject1.Users.Add(user);
                subject2.Users.Add(user);

                subject1 = db.Subjects.Add(subject1);
                subject2 = db.Subjects.Add(subject2);

                Assert.AreNotEqual(db.SaveChanges(), 0);

                Assert.AreNotEqual(subject1.Users.Count, 0);
                Assert.AreNotEqual(subject2.Users.Count, 0);

                var ev = new Event
                {
                    Id = Guid.NewGuid(),
                    Title = "title",
                    Message = "message",
                    DateCreated = DateTime.Now,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    NotificationStartDate = DateTime.Now,
                };

                ev = db.Events.Add(ev);
                Assert.AreNotEqual(db.SaveChanges(), 0);

                ev.Subjects.Add(subject1);
                ev.Subjects.Add(subject2);

                db.Events.AddOrUpdate(ev);
                Assert.AreNotEqual(db.SaveChanges(), 0);
                Assert.AreNotEqual(ev.Subjects.Count, 0);

                db.Events.Remove(ev);
                db.Subjects.Remove(subject1);
                db.Subjects.Remove(subject2);
                db.Users.Remove(user);

                Assert.AreNotEqual(db.SaveChanges(), 0);
            }
        }
    }
}
