using System.Data.Entity;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.DAL.ContextInitializers
{
    public static class EventsContextInitializer
    {
        public static void InitializeContext(DbModelBuilder builder)
        {
            builder.Entity<Event>().HasKey(k => k.Id);

            builder.Entity<Event>().Property(p => p.Title).IsRequired();
            builder.Entity<Event>().Property(p => p.Message).IsRequired();
            builder.Entity<Event>().Property(p => p.DateCreated).IsRequired();
            builder.Entity<Event>().Property(p => p.StartDate).IsRequired();
            builder.Entity<Event>().Property(p => p.EndDate).IsRequired();
            builder.Entity<Event>().Property(p => p.NotificationStartDate).IsRequired();
            builder.Entity<Event>().Property(p => p.Place).IsOptional();

            // Map to the Subjects table
            builder.Entity<Event>()
                .HasMany(p => p.Subjects)
                .WithMany(p => p.Events)
                .Map(m =>
                {
                    m.ToTable("Subject");
                    m.MapLeftKey("EventId");
                    m.MapRightKey("SubjectId");
                });
        }
    }
}