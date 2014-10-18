using System;
using System.Data.Entity.Migrations;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.DAL.Model.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollegeBuffer.Tests.Models
{
    [TestClass]
    public class CommentsTest
    {
        [TestMethod]
        public void PerformCommentsTest()
        {
            Comment comment1;
            Comment comment2;
            Comment comment3;
            Comment comment4;
            var user = CreateEntitties(out comment1, out comment2, out comment3, out comment4);

            using (var db = new DatabaseContext())
            {
                user = db.Users.Add(user);

                Assert.AreNotEqual(db.SaveChanges(), 0);

                comment1.User = user;
                comment2.User = user;
                comment3.User = user;
                comment4.User = user;

                comment1 = db.Comments.Add(comment1);
                comment2 = db.Comments.Add(comment2);
                comment3 = db.Comments.Add(comment3);
                comment4 = db.Comments.Add(comment4);

                Assert.AreNotEqual(db.SaveChanges(), 0);

                comment2.ParentComment = comment1;
                comment2.Replies.Add(comment3);
                comment2.Replies.Add(comment4);

                db.Comments.AddOrUpdate(comment1);
                db.Comments.AddOrUpdate(comment2);
                db.Comments.AddOrUpdate(comment3);
                db.Comments.AddOrUpdate(comment4);

                Assert.AreNotEqual(db.SaveChanges(), 0);

                Assert.AreEqual(comment1.Replies.Count, 1);
                Assert.AreEqual(comment2.ParentComment.Id, comment1.Id);
                Assert.AreEqual(comment2.Replies.Count, 2);

                db.Comments.Remove(comment1);
                db.Comments.Remove(comment2);
                db.Comments.Remove(comment3);
                db.Comments.Remove(comment4);
                db.Users.Remove(user);

                Assert.AreNotEqual(db.SaveChanges(), 0);
            }
        }

        private static User CreateEntitties(out Comment comment1, out Comment comment2, out Comment comment3,
            out Comment comment4)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "user",
                Password = "pass",
                EMail = "email",
                Role = UserRoles.Student
            };

            comment1 = new Comment
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Text = "something1"
            };

            comment2 = new Comment
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Text = "somethin2"
            };

            comment3 = new Comment
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Text = "something3"
            };

            comment4 = new Comment
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Text = "something4"
            };
            return user;
        }
    }
}
