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

                if (session == null)
                    throw new Exception("Session not created!");

                user = db.SessionsRepository.GetUser(session.Id, session.SessionKey);
                if (user == null)
                    throw new Exception("Session not found after creation!");

                if (!db.SessionsRepository.Logout(user.Username))
                    throw new Exception("Session logout failed!");

            }
        }
    }
}
