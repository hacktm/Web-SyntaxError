using System.Data.Entity;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.DAL.ContextInitializers
{
    public static class NotificationsContextInitializer
    {
        public static void InitializeContext(DbModelBuilder builder)
        {
            InitializeAnnouncementNotifications(builder);
            InitializeEventNotifications(builder);
        }

        private static void InitializeAnnouncementNotifications(DbModelBuilder builder)
        {
            builder.Entity<AnnouncementNotification>().HasKey(k => k.Id);

            builder.Entity<AnnouncementNotification>().Property(p => p.Message).IsRequired();

            // Map to the Users table
            builder.Entity<AnnouncementNotification>()
                .HasRequired(p => p.User)
                .WithMany(p => p.AnnouncementNotifications)
                .Map(m => m.MapKey("UserId"))
                .WillCascadeOnDelete(true);
        }

        public static void InitializeEventNotifications(DbModelBuilder builder)
        {
            builder.Entity<EventNotification>().HasKey(k => k.Id);

            builder.Entity<EventNotification>().Property(p => p.Message).IsRequired();

            // Map to the Users table
            builder.Entity<EventNotification>()
                .HasRequired(p => p.User)
                .WithMany(p => p.EventNotifications)
                .Map(m => m.MapKey("UserId"))
                .WillCascadeOnDelete(true);
        }
    }
}