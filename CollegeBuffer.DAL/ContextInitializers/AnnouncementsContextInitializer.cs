using System.Data.Entity;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.DAL.ContextInitializers
{
    public static class AnnouncementsContextInitializer
    {
        public static void InitializeContext(DbModelBuilder builder)
        {
            builder.Entity<Announcement>().HasKey(k => k.Id);

            builder.Entity<Announcement>().Property(p => p.Date).IsRequired();
            builder.Entity<Announcement>().Property(p => p.Title).IsRequired();
            builder.Entity<Announcement>().Property(p => p.Message).IsRequired();

            // Map to the Users table
            builder.Entity<Announcement>()
                .HasRequired(p => p.User)
                .WithMany(p => p.Announcements)
                .Map(m => m.MapKey("UserId"));

            // Map to the Groups table
            builder.Entity<Announcement>()
                .HasRequired(p => p.Group)
                .WithMany(p => p.Announcements)
                .Map(m => m.MapKey("GroupId"));
        }
    }
}