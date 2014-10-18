using System;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.DAL.Model.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollegeBuffer.Tests.Models
{
    [TestClass]
    public class GroupRequestsTest
    {
        [TestMethod]
        public void TestRequests()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "user",
                Password = "pass",
                EMail = "email",
                Role = UserRoles.Professor
            };

            var group = new Group
            {
                Id = Guid.NewGuid(),
                Name = "group"
            };

            var request = new GroupRequest
            {
                Date = DateTime.Now,
                Group = group,
                User = user
            };

            using (var db = new DatabaseContext())
            {
                user = db.Users.Add(user);
                group = db.Groups.Add(group);
                request = db.GroupRequests.Add(request);

                if (db.SaveChanges() == 0)
                    throw new Exception("The request couldn't be updated!");

                Assert.AreEqual(request.User.Id, user.Id);
                Assert.AreEqual(request.Group.Id, group.Id);

                db.GroupRequests.Remove(request);
                db.Users.Remove(user);
                db.Groups.Remove(group);

                if (db.SaveChanges() == 0)
                    throw new Exception("Could not delete entities!");
            }
        }
    }
}
