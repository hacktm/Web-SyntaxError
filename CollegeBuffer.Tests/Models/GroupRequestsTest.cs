using System;
using CollegeBuffer.BLL;
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

            using (var db = DbUnitOfWork.NewInstance())
            {
                user = db.UsersRepository.Insert(user);
                group = db.GroupsRepository.Insert(group);
                request = db.GroupRequestsRepository.Insert(request);

                Assert.AreNotEqual(db.SaveAllChanges(), false);

                Assert.AreEqual(request.User.Id, user.Id);
                Assert.AreEqual(request.Group.Id, group.Id);

                Assert.AreEqual(db.GroupRequestsRepository.Delete(request), true);
                Assert.AreEqual(db.UsersRepository.Delete(user), true);
                Assert.AreEqual(db.GroupsRepository.Delete(group), true);

                Assert.AreNotEqual(db.SaveAllChanges(), false);
            }
        }
    }
}
