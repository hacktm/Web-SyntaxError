using System;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.DAL.Model.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollegeBuffer.Tests.Models
{
    [TestClass]
    public class AnnouncementsTest
    {
        [TestMethod]
        public void TestAnnouncements()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "user1",
                Password = "pass",
                EMail = "user1@user.com",
                Role = UserRoles.Student,
            };

            var group = new Group { Id = Guid.NewGuid(), Name = "group1" };

            var announcement = new Announcement
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Title = "title",
                Message = "message"
            };

            using (var db = new DatabaseContext())
            {
                user = db.Users.Add(user);
                group = db.Groups.Add(group);

                Assert.AreNotEqual(db.SaveChanges(), 0);

                announcement.User = user;
                announcement.Group = group;

                announcement = db.Announcements.Add(announcement);

                Assert.AreNotEqual(db.SaveChanges(), 0);
                Assert.AreEqual(announcement.User.Id, user.Id);
                Assert.AreEqual(announcement.Group.Id, group.Id);

                db.Announcements.Remove(announcement);
                db.Users.Remove(user);
                db.Groups.Remove(group);

                Assert.AreNotEqual(db.SaveChanges(), 0);
            }
        }
    }
}
