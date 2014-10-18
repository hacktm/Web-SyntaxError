using System;
using System.Data.Entity.Migrations;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.DAL.Model.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollegeBuffer.Tests.Models
{
    [TestClass]
    public class UsersGroupsTest
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

            using (var db = new DatabaseContext())
            {
                _user1 = db.Users.Add(_user1);
                _user2 = db.Users.Add(_user2);
                _user3 = db.Users.Add(_user3);

                _group1 = db.Groups.Add(_group1);
                _group2 = db.Groups.Add(_group2);

                if (db.SaveChanges() == 0)
                    throw new Exception("Not all entities have been created");
            }
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

                if (db.SaveChanges() == 0)
                    throw new Exception("Not all entities have been created");
            }
        }

        [TestMethod]
        public void CreateRelations()
        {
            _group1.Administrators.Add(_user1);
            _user2.GroupsAsAdministrator.Add(_group2);

            _group1.Users.Add(_user2);
            _group2.Users.Add(_user1);
            _group2.Users.Add(_user3);

            using (var db = new DatabaseContext())
            {
                db.Groups.AddOrUpdate(_group1);
                db.Groups.AddOrUpdate(_group2);
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
