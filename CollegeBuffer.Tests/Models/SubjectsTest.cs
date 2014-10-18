using System;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.DAL.Model.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollegeBuffer.Tests.Models
{
    [TestClass]
    public class SubjectsTest
    {
        private static User _user1;
        private static User _user2;
        private static User _user3;

        private static Group _group1;
        private static Group _group2;

        [ClassInitialize]
        public static void CreateEntities(TestContext testContext)
        {
            GenerateUsers();
            GenerateGroups();
        }

        [ClassCleanup]
        public static void RemoveEntities()
        {
            using (var db = new DatabaseContext())
            {
                db.Users.Remove(_user1);
                db.Users.Remove(_user2);
                db.Users.Remove(_user3);

                db.Groups.Remove(_group1);
                db.Groups.Remove(_group2);

                Assert.AreNotEqual(db.SaveChanges(), 0);
            }
        }

        [TestMethod]
        public void CreateRelations()
        {
            var subject = new Subject
            {
                Id = Guid.NewGuid(),
                Name = "subj",
            };

            subject.Users.Add(_user1);
            subject.Users.Add(_user2);
            subject.Users.Add(_user3);
            subject.Groups.Add(_group1);
            subject.Groups.Add(_group2);

            using (var db = new DatabaseContext())
            {
                subject = db.Subjects.Add(subject);

                Assert.AreNotEqual(db.SaveChanges(), 0);

                Assert.AreEqual(subject.Users.Count, 3);
                Assert.AreEqual(subject.Groups.Count, 2);

                db.Subjects.Remove(subject);

                Assert.AreNotEqual(db.SaveChanges(), 0);
            }
        }

        private static void GenerateGroups()
        {
            _group1 = new Group { Id = Guid.NewGuid(), Name = "group1" };
            _group2 = new Group { Id = Guid.NewGuid(), Name = "group2" };
        }

        private static void GenerateUsers()
        {
            _user1 = new User
            {
                Id = Guid.NewGuid(),
                Username = "user1",
                Password = "pass",
                EMail = "user1@user.com",
                Role = UserRoles.Student,
            };

            _user2 = new User
            {
                Id = Guid.NewGuid(),
                Username = "user2",
                Password = "pass",
                EMail = "user2@user.com",
                Role = UserRoles.Student
            };

            _user3 = new User
            {
                Id = Guid.NewGuid(),
                Username = "user3",
                Password = "pass",
                EMail = "user3@user.com",
                Role = UserRoles.Student
            };
        }
    }
}
