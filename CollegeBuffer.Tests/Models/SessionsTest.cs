using System;
using CollegeBuffer.BLL;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.DAL.Model.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollegeBuffer.Tests.Models
{
    [TestClass]
    public class SessionsTest
    {
        [TestMethod]
        public void PerformTest()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "user",
                Password = "pass",
                EMail = "user@user.com",
                Role = UserRoles.Student
            };

            using (var db = DbUnitOfWork.NewInstance())
            {
                user = db.UsersRepository.Insert(user);
                var session = db.SessionsRepository.Login(user.Username);

                Assert.AreNotEqual(session, null);

                user = db.SessionsRepository.GetUser(session.Id, session.SessionKey);
                
                Assert.AreNotEqual(user, null);

                Assert.AreEqual(db.SessionsRepository.Logout(user.Username), true);
                Assert.AreEqual(db.UsersRepository.Delete(user), true);

            }
        }
    }
}
