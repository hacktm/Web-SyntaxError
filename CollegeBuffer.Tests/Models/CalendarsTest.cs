using System;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.DAL.Model.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollegeBuffer.Tests.Models
{
    [TestClass]
    public class CalendarsTest
    {
        [TestMethod]
        public void TestCalendars()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "user1",
                Password = "pass",
                EMail = "user1@user.com",
                Role = UserRoles.Student
            };

            var calendar = new Calendar
            {
                Id = Guid.NewGuid(),
                User = user
            };

            var ev = new Event
            {
                DateCreated = DateTime.Now,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                NotificationStartDate = DateTime.Now
            };

            using (var db = new DatabaseContext())
            {
                user = db.Users.Add(user);
                calendar = db.Calendars.Add(calendar);

                Assert.AreNotEqual(db.SaveChanges(), 0);

                var calendarEntries = new CalendarEntry[500];
                for (var i = 0; i < calendarEntries.Length; i++)
                {
                    calendarEntries[i] = new CalendarEntry
                    {
                        Id = Guid.NewGuid(),
                        Calendar = calendar,
                        Text = "entry" + i
                    };

                    calendarEntries[i].CopyEvent(ev);

                    db.CalendarEntries.Add(calendarEntries[i]);
                }

                Assert.AreNotEqual(db.SaveChanges(), 0);

                foreach (var t in calendarEntries)
                {
                    db.CalendarEntries.Remove(t);
                }

                db.Calendars.Remove(calendar);
                db.Users.Remove(user);

                Assert.AreNotEqual(db.SaveChanges(), 0);
            }
        }
    }
}
