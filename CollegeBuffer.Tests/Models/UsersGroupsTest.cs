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
        private static User user1;
        private static User user2;
        private static User user3;

        private static Group group1;
        private static Group group2;

        [ClassInitialize]
        public static void CreateEntities(TestContext testContext)
        {
            GenerateUsers();
            GenerateGroups();

            using (var db = new DatabaseContext())
            {
                user1 = db.Users.Add(user1);
                user2 = db.Users.Add(user2);
                user3 = db.Users.Add(user3);

                group1 = db.Groups.Add(group1);
                group2 = db.Groups.Add(group2);

                if (db.SaveChanges() == 0)
                    throw new Exception("Not all entities have been created");
            }
        }

        [ClassCleanup]
        public static void RemoveEntities()
        {
            using (var db = new DatabaseContext())
            {
                db.Users.Remove(user1);
                db.Users.Remove(user2);
                db.Users.Remove(user3);

                db.Groups.Remove(group1);
                db.Groups.Remove(group2);

                if (db.SaveChanges() == 0)
                    throw new Exception("Not all entities have been created");
            }
        }

        [TestMethod]
        public void CreateRelations()
        {
            group1.Administrators.Add(user1);
            user2.GroupsAsAdministrator.Add(group2);

            group1.Users.Add(user2);
            group2.Users.Add(user1);
            group2.Users.Add(user3);

            using (var db = new DatabaseContext())
            {
                db.Groups.AddOrUpdate(group1);
                db.Groups.AddOrUpdate(group2);
            }
        }

        private static void GenerateGroups()
        {
            group1 = new Group { Id = Guid.NewGuid(), Name = "group1" };
            group2 = new Group { Id = Guid.NewGuid(), Name = "group2" };
        }

        private static void GenerateUsers()
        {
            user1 = new User
            {
                Id = Guid.NewGuid(),
                Username = "user1",
                Password = "pass",
                EMail = "user1@user.com",
                Role = UserRoles.Student
            };

            user2 = new User
            {
                Id = Guid.NewGuid(),
                Username = "user2",
                Password = "pass",
                EMail = "user2@user.com",
                Role = UserRoles.Student
            };

            user3 = new User
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
