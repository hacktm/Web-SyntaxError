//#define TESTS
#undef TESTS
#define MOCK
//#undef MOCK
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.DAL.Model.Enums;
using CollegeBuffer.DAL.Special;

namespace CollegeBuffer.DAL.ContextInitializers
{
#if TESTS

    internal class DatabaseContextInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {

#else

    internal class DatabaseContextInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
#endif

        public override void InitializeDatabase(DatabaseContext context)
        {
            base.InitializeDatabase(context);

            CreateImplicitAdmin();

#if MOCK
            var db = new DatabaseContext();

            if (!db.Groups.Any())
            {
                PopulateGroups();
                PopulateUsers();
                PopulateCalendar();
                PopulateEvents();
                PopulateAnnouncements();
            }

            db.Dispose();
#endif
        }

        private void PopulateAnnouncements()
        {
            var ann1 = new Announcement
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Title = "New HeadStudent",
                Message = "Codo became Head Student",
            };

            var ann2 = new Announcement
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Title = "More members",
                Message = "Neagu became a new member of the group CTI",
            };

            var ann3 = new Announcement
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Title = "Welcome!",
                Message = "Welcome to CollegeBuffer. Have a nice stay!",
            };

            var com1 = new Comment
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Text = "Awesome comment nr. 1"
            };

            var com2 = new Comment
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Text = "Awesome comment nr. 2"
            };

            var com3 = new Comment
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Text = "Awesome comment nr. 3"
            };

            var com4 = new Comment
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Text = "Awesome comment nr. 4"
            };

            using (var db = new DatabaseContext())
            {
                var group = db.Groups.FirstOrDefault(g => g.Name == "AC");
                var upt = db.Groups.FirstOrDefault(g => g.Name == "UPT");
                var admin = db.Users.FirstOrDefault(u => u.Role == UserRoles.Administrator);
                var codo = db.Users.FirstOrDefault(u => u.Username == "coddo");
                var neagu = db.Users.FirstOrDefault(u => u.Username == "neagu");

                com1.Replies.Add(com3);
                com1.User = codo;
                com2.User = neagu;
                com3.User = neagu;
                com4.User = codo;

                ann1.User = ann2.User = ann3.User = admin;
                ann1.Group = ann3.Group = group;
                ann2.Group = upt;
                ann1.Comments.Add(com1);
                ann1.Comments.Add(com4);
                ann3.Comments.Add((com2));

                db.Announcements.Add(ann1);
                db.Announcements.Add(ann2);
                db.Announcements.Add(ann3);

                db.SaveChanges();

                ann1 = db.Announcements.Find(ann1.Id);
                ann2 = db.Announcements.Find(ann2.Id);
                ann3 = db.Announcements.Find(ann3.Id);
            }

            PopulateNotifications(new[] { ann1, ann2, ann3 });
        }

        private static void PopulateCalendar()
        {
            var cal1 = new Calendar
            {
                Id = Guid.NewGuid(),
            };
            var cal2 = new Calendar
            {
                Id = Guid.NewGuid(),
            };

            using (var db = new DatabaseContext())
            {
                cal1.User = db.Users.First(u => u.Username == "coddo");
                cal2.User = db.Users.First(u => u.Username == "neagu");

                db.Calendars.Add(cal1);
                db.Calendars.Add(cal2);

                db.SaveChanges();
            }
        }

        private static void PopulateNotifications(IList<Event> events)
        {
            var notifs = new EventNotification[2 * events.Count];
            var calendarEntries = new CalendarEntry[2 * events.Count];

            using (var db = new DatabaseContext())
            {
                var codo = db.Users.FirstOrDefault(u => u.Username == "coddo");
                var neagu = db.Users.FirstOrDefault(u => u.Username == "neagu");

                for (var i = 0; i < events.Count; i++)
                {
                    notifs[i] = new EventNotification
                    {
                        Id = Guid.NewGuid(),
                        User = codo,
                        Event = db.Events.Find(events[i].Id),
                        Message = "Event number: " + i
                    };

                    calendarEntries[i] = new CalendarEntry
                    {
                        Id = Guid.NewGuid(),
                        Calendar = codo.Calendar
                    };
                    calendarEntries[i].CopyEvent(events[i]);

                    db.CalendarEntries.Add(calendarEntries[i]);
                    db.EventNotifications.Add(notifs[i]);
                }

                for (var i = 3; i < 2 * events.Count; i++)
                {
                    notifs[i] = new EventNotification
                    {
                        Id = Guid.NewGuid(),
                        User = neagu,
                        Event = db.Events.Find(events[i - 3].Id),
                        Message = "Event number: " + (i - 3)
                    };

                    calendarEntries[i] = new CalendarEntry
                    {
                        Id = Guid.NewGuid(),
                        Calendar = neagu.Calendar
                    };
                    calendarEntries[i].CopyEvent(events[i - 3]);

                    db.CalendarEntries.Add(calendarEntries[i]);
                    db.EventNotifications.Add(notifs[i]);
                }

                db.SaveChanges();
            }
        }

        private static void PopulateNotifications(IList<Announcement> announcements)
        {
            var notifs = new AnnouncementNotification[2 * announcements.Count];

            using (var db = new DatabaseContext())
            {
                var codo = db.Users.FirstOrDefault(u => u.Username == "coddo");
                var neagu = db.Users.FirstOrDefault(u => u.Username == "neagu");

                for (var i = 0; i < announcements.Count; i++)
                {
                    notifs[i] = new AnnouncementNotification
                    {
                        Id = Guid.NewGuid(),
                        User = codo,
                        Announcement = db.Announcements.Find(announcements[i].Id),
                        Message = "Announcement number: " + i
                    };

                    db.AnnouncementNotifications.Add(notifs[i]);
                }

                for (var i = 3; i < 2 * announcements.Count; i++)
                {
                    notifs[i] = new AnnouncementNotification
                    {
                        Id = Guid.NewGuid(),
                        User = neagu,
                        Announcement = db.Announcements.Find(announcements[i - 3].Id),
                        Message = "Announcement number: " + (i - 3)
                    };

                    db.AnnouncementNotifications.Add(notifs[i]);
                }

                db.SaveChanges();
            }

        }

        private static void PopulateEvents()
        {
            var exam1 = new Event
            {
                Id = Guid.NewGuid(),
                Title = "Maths exam",
                Message = "You have a math exam coming up!",
                DateCreated = DateTime.Now,
                StartDate = DateTime.Now.AddDays(10),
                EndDate = DateTime.Now.AddDays(10),
                NotificationStartDate = DateTime.Now.AddDays(5),
                Place = "UPT"
            };

            var exam2 = new Event
            {
                Id = Guid.NewGuid(),
                Title = "Lab test for OOP",
                Message = "You have a basic test coming up for OOP!",
                DateCreated = DateTime.Now,
                StartDate = DateTime.Now.AddDays(6),
                EndDate = DateTime.Now.AddDays(6),
                NotificationStartDate = DateTime.Now.AddDays(4),
                Place = "UPT"
            };

            var boboParty = new Event
            {
                Id = Guid.NewGuid(),
                Title = "Bobo party",
                Message = "Party with the first year students!!",
                DateCreated = DateTime.Now,
                StartDate = DateTime.Now.AddDays(20),
                EndDate = DateTime.Now.AddDays(21),
                NotificationStartDate = DateTime.Now.AddDays(15),
                Place = "Disco Club"
            };

            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                Text = "Rock the party!!!",
                Date = DateTime.Now
            };

            boboParty.Comments.Add(comment);

            using (var db = new DatabaseContext())
            {
                comment.User = db.Users.First();
                exam1.Group = exam2.Group = db.Groups.FirstOrDefault(g => g.Name == "CTI");
                boboParty.Group = db.Groups.FirstOrDefault(g => g.Name == "AC");

                db.Events.Add(exam1);
                db.Events.Add(exam2);
                db.Events.Add(boboParty);

                db.SaveChanges();

                exam1 = db.Events.Find(exam1.Id);
                exam2 = db.Events.Find(exam2.Id);
                boboParty = db.Events.Find(boboParty.Id);
            }

            PopulateNotifications(new[] { exam1, exam2, boboParty });
        }

        private static void PopulateGroups()
        {
            var upt = new Group
            {
                Id = Guid.NewGuid(),
                Name = "UPT"
            };

            var uvt = new Group
            {
                Id = Guid.NewGuid(),
                Name = "UVT"
            };

            upt.SubGroups.Add(new Group
            {
                Id = Guid.NewGuid(),
                Name = "AC"
            });
            upt.SubGroups.Add(new Group
            {
                Id = Guid.NewGuid(),
                Name = "ETC"
            });
            upt.SubGroups[0].SubGroups.Add(new Group
            {
                Id = Guid.NewGuid(),
                Name = "CTI"
            });
            uvt.SubGroups.Add(new Group
            {
                Id = Guid.NewGuid(),
                Name = "Info"
            });

            using (var db = new DatabaseContext())
            {
                db.Groups.Add(upt);
                db.Groups.Add(uvt);

                db.SaveChanges();
            }
        }

        private static void PopulateUsers()
        {
            var codo = new User
            {
                Id = Guid.NewGuid(),
                Username = "coddo",
                Password = TokenGenerator.EncryptMd5("coddo"),
                Role = UserRoles.HeadStudent,
                EMail = "c@c.c"
            };

            var neagu = new User
            {
                Id = Guid.NewGuid(),
                Username = "neagu",
                Password = TokenGenerator.EncryptMd5("neagu"),
                Role = UserRoles.Student,
                EMail = "n@n.com"
            };

            using (var db = new DatabaseContext())
            {
                db.Users.Add(codo);
                db.Users.Add(neagu);

                db.SaveChanges();
                codo = db.Users.Find(codo.Id);
                neagu = db.Users.Find(neagu.Id);

                var upt = db.Groups.FirstOrDefault(g => g.Name == "UPT");

                var ac = upt.SubGroups.FirstOrDefault(g => g.Name == "AC");
                var cti = ac.SubGroups.FirstOrDefault(g => g.Name == "CTI");

                ac.Users.Add(codo);
                cti.Users.Add(neagu);

                db.Groups.AddOrUpdate(upt);
                db.SaveChanges();
            }
        }

        private static void CreateImplicitAdmin()
        {
            var admin = new User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Password = TokenGenerator.EncryptMd5("admin"),
                EMail = "",
                Role = UserRoles.Administrator
            };

            using (var db = new DatabaseContext())
            {
                if (db.Users.Any(u => u.Role == UserRoles.Administrator)) return;

                admin = db.Users.Add(admin);

                if (admin == null || db.SaveChanges() == 0)
                    throw new ApplicationException("The admin account could not be initialized!");
            }
        }
    }

}