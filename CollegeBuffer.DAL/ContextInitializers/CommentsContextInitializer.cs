using System.Data.Entity;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.DAL.ContextInitializers
{
    public static class CommentsContextInitializer
    {
        public static void InitializeContext(DbModelBuilder builder)
        {
            builder.Entity<Comment>().HasKey(k => k.Id);

            builder.Entity<Comment>().Property(p => p.Date).IsRequired();
            builder.Entity<Comment>().Property(p => p.Text).IsRequired();

            // Map to the Users table
            builder.Entity<Comment>()
                .HasRequired(p => p.User)
                .WithMany(p => p.Comments)
                .Map(m => m.MapKey("UserId"));

            // Map to Announcements table
            builder.Entity<Comment>()
                .HasOptional(p => p.Announcement)
                .WithMany(p => p.Comments)
                .Map(m => m.MapKey("AnnouncementId"));

            // Map to Events table
            builder.Entity<Comment>()
                .HasOptional(p => p.Event)
                .WithMany(p => p.Comments)
                .Map(m => m.MapKey("EventId"));

            // Map to self
            builder.Entity<Comment>()
                .HasOptional(p => p.ParentComment)
                .WithMany(p => p.Replies)
                .Map(m => m.MapKey("ParentCommentId"));
        }
    }
}