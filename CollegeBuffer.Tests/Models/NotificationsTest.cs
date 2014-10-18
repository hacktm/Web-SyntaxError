using System;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.DAL.Model.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollegeBuffer.Tests.Models
{
    [TestClass]
    public class NotificationsTest
    {
        private static User _user1;
        private static User _user2;
        private static Group _group;
        private static Announcement _announcement;
        private static Event _event;

        [ClassInitialize]
        public static void CreateEntities(TestContext testContext)
        {
            _user1 = new User
            {
                Id = Guid.NewGuid(),
                Username = "user1",
                Password = "pass",
                EMail = "user1@user.com",
                Role = UserRoles.Student
            };

            _user2 = new User
            {
                Id = Guid.NewGuid(),
                Username = "user2",
                Password = "pass",
                EMail = "user2@user.com",
                Role = UserRoles.Student
            };

            _group = new Group { Id = Guid.NewGuid(), Name = "group" };

            _announcement = new Announcement
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Title = "title",
                Message = "message",
                Group = _group,
                User = _user1
            };

            _event = new Event
            {
                Id = Guid.NewGuid(),
                Title = "title",
                Message = "message",
                DateCreated = DateTime.Now,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                NotificationStartDate = DateTime.Now,
            };

            using (var db = new DatabaseContext())
            {
                _user1 = db.Users.Add(_user1);
                _user2 = db.Users.Add(_user2);
                _group = db.Groups.Add(_group);
                _announcement = db.Announcements.Add(_announcement);
                _event = db.Events.Add(_event);

                Assert.AreNotEqual(db.SaveChanges(), 0);
            }
        }

        [ClassCleanup]
        public static void RemoveEntities()
        {
            using (var db = new DatabaseContext())
            {
                db.Events.Remove(_event);
                db.Announcements.Remove(_announcement);
                db.Groups.Remove(_group);
                db.Users.Remove(_user1);
                
                Assert.AreNotEqual(db.SaveChanges(), 0);
            }
        }

        [TestMethod]
        public void TestNotifications()
        {
            var announcementNotification = new AnnouncementNotification
            {
                Id = Guid.NewGuid(),
                Message = "msg",
            };

            var eventNotification = new EventNotification
            {
                Id = Guid.NewGuid(),
                Message = "msg2",
            };

            using (var db = new DatabaseContext())
            {
                // Workaround for EF bugs
                _user1 = db.Users.Find(_user1.Id);
                _user2 = db.Users.Find(_user2.Id);
                _group = db.Groups.Find(_group.Id);
                _event = db.Events.Find(_event.Id);
                _announcement = db.Announcements.Find(_announcement.Id);
                //

                announcementNotification.Announcement = _announcement; 
                eventNotification.Event = _event;

                announcementNotification.User = _user1;
                eventNotification.User = _user2;

                db.AnnouncementNotifications.Add(announcementNotification);
                db.EventNotifications.Add(eventNotification);

                Assert.AreNotEqual(db.SaveChanges(), 0);

                Assert.AreEqual(announcementNotification.User.Id, _user1.Id);
                Assert.AreEqual(announcementNotification.Message, "msg");
                Assert.AreEqual(eventNotification.User.Id, _user2.Id);
                Assert.AreEqual(eventNotification.Message, "msg2");

                db.AnnouncementNotifications.Remove(announcementNotification);
                db.EventNotifications.Remove(eventNotification);

                Assert.AreNotEqual(db.SaveChanges(), 0);
            }
        }
    }
}
